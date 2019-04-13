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

    private void Awake()
    {
        lanAddresses = new Dictionary<LanBroadcastInfo, float>();
        broadcastTimer = 5f;
        SearchForBroadcast();
        StartCoroutine(CleanUpExpiredBroadcasts());
    }

    public void StartBroadcast()
    {
        StopBroadcast(); // Clean up
        base.Initialize();
        base.StartAsServer(); // Send broadcast
    }

    public void SearchForBroadcast()
    {
        if (base.running)
        {
            StopBroadcast();
        }
            

        base.Initialize();
        base.StartAsClient(); // Listen for broadcasts
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

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);

        LanBroadcastInfo broadcastInfo = new LanBroadcastInfo(fromAddress, data);

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

        Debug.Log("Broadcasting");
    }

    private void UpdateGameList()
    {
        AvailableGamesList.HandleNewGamesList(lanAddresses.Keys.ToList());
    }
}