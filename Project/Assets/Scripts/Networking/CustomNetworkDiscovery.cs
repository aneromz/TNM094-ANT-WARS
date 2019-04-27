using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery
{
    private float broadcastTimer;
    private Dictionary<LanBroadcastInfo, float> lanAddresses;
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

        broadcastTimer = 5f;

        Reset();
    }

    public void StartBroadcast()
    {
        StopBroadcast(); // Clean up
        CleanUpCoroutines();

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
        lanAddresses = new Dictionary<LanBroadcastInfo, float>();
        shouldJoinGame = false;

        SearchForBroadcast();
        StartCoroutine(CleanUpExpiredBroadcasts());
    }


    private IEnumerator CleanUpExpiredBroadcasts()
    {
        while (true)
        {
            bool listHasChanged = false;

            var keys = lanAddresses.Keys.ToList();
            foreach (var key in keys)
            {
                // If time has expired, remove the ip address
                if (lanAddresses[key] <= Time.time)
                {
                    lanAddresses.Remove(key);
                    listHasChanged = true;
                }
            }

            if (listHasChanged)
                UpdateGameList();

            yield return new WaitForSeconds(broadcastTimer);
        }
    }

    public void CleanUpCoroutines()
    {
        StopCoroutine(CleanUpExpiredBroadcasts());
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);

        if (shouldJoinGame == false)
            return;

        LanBroadcastInfo broadcastInfo = new LanBroadcastInfo(fromAddress, data);

        var manager = FindObjectOfType<CustomNetworkManagerUI>();
        manager.ToggleGameSearch(); // Stop search for game
        manager.JoinGame(broadcastInfo); // Join game

        /*
        // Check if the broadcasted address isn't already in the dictionary
        if (lanAddresses.ContainsKey(broadcastInfo) == false)
        {
            lanAddresses.Add(broadcastInfo, Time.time + broadcastTimer);
            UpdateGameList();
        }
        else // If the address exists in the dictionary update the timer
        {
            lanAddresses[broadcastInfo] = Time.time + broadcastTimer;
        }
        
        Debug.Log("Broadcast recieved");
        */
    }

    public void JoinGameOnRecievedBroadcast(bool flag)
    {
        shouldJoinGame = flag;
    }

    private void UpdateGameList()
    {
        AvailableGamesList.HandleNewGamesList(lanAddresses.Keys.ToList());
    }
}