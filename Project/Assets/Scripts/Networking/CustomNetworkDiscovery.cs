using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery
{
    private bool shouldJoinGame;

    public static CustomNetworkDiscovery instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        Reset();
    }

    public void StartBroadcast()
    {
        StopBroadcast(); // Clean up

        Initialize();
        StartAsServer(); // Send broadcast
    }

    public void SearchForBroadcast()
    {
        Initialize();
        StartAsClient(); // Listen for broadcasts
    }

    public void Reset()
    {
        shouldJoinGame = false;

        SearchForBroadcast();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);

        if (shouldJoinGame == false)
            return;

        LanBroadcastInfo broadcastInfo = new LanBroadcastInfo(fromAddress, data);

        var manager = FindObjectOfType<CustomNetworkManagerUI>();
        manager.ToggleGameSearch(); // Stop search for game
        manager.JoinLobby(broadcastInfo); // Join game
    }

    public void JoinGameOnRecievedBroadcast(bool flag)
    {
        shouldJoinGame = flag;
    }
}