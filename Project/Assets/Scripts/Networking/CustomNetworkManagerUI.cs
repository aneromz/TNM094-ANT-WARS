using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNetworkManagerUI : MonoBehaviour
{
    [SerializeField]
    private NetworkManager manager;
    [SerializeField]
    private CustomNetworkDiscovery discovery;

    [SerializeField]
    public bool showGUI = true;
    [SerializeField]
    public int offsetX = 0;
    [SerializeField]
    public int offsetY = 0;

    // Menu stuff
    [SerializeField]
    private Button hostButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Image gameBrowser;
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private HeadUpDisplay hud;

    private bool menuIsVisible;
    public static CustomNetworkManagerUI instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        // Add onclick functions to buttons
        hostButton.onClick.AddListener(HostGame);
        exitButton.onClick.AddListener(ExitGame);

        // Hide information thats not suppose to be visible
        exitButton.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
        menuIsVisible = true;
        menu.gameObject.SetActive(menuIsVisible);
    }

    public void HostGame ()
    {
        manager.StartHost();
        discovery.StartBroadcast();

        // Manage buttons
        hostButton.gameObject.SetActive(false);
        gameBrowser.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        hud.gameObject.SetActive(true);

        ToggleMenu();
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        // Manage buttons visibility
        hostButton.gameObject.SetActive(true);
        gameBrowser.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);

        // Load start menu scene
        SceneManager.LoadScene("StartMenu");

        manager.StopHost();
        discovery.StopBroadcast();
        discovery.Reset();
    }

    public void JoinGame(LanBroadcastInfo gameInfo)
    {
        // Configure network settings
        manager.networkAddress = gameInfo.ipAddress;
        manager.networkPort = gameInfo.port;
        manager.StartClient();

        // Manage buttons visibility
        hostButton.gameObject.SetActive(false);
        gameBrowser.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        hud.gameObject.SetActive(true);

        ToggleMenu();

        discovery.StopBroadcast(); // Stop listen for broadcasts
        discovery.CleanUpCoroutines();

        SceneManager.LoadScene("Game");
    }

    public void ToggleMenu()
    {
        menuIsVisible = menuIsVisible ? false : true;
        menu.gameObject.SetActive(menuIsVisible);
    }

    void OnGUI()
    {
        if (!showGUI)
            return;

        int xpos = 10 + offsetX;
        int ypos = 40 + offsetY;
        int spacing = 24;

        if (NetworkClient.active)
        {
            GUI.Label(new Rect(xpos, ypos, 300, 20), "Address: " + manager.networkAddress + " port: " + manager.networkPort);
            ypos += spacing;
        }

        if (NetworkClient.active && !ClientScene.ready)
        {
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready"))
            {
                ClientScene.Ready(manager.client.connection);

                if (ClientScene.localPlayers.Count == 0)
                {
                    ClientScene.AddPlayer(0);
                }
            }
            ypos += spacing;
        }
    }
}