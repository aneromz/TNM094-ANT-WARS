using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Player Connected");
    }

    private void OnLevelWasLoaded(int level)
    {
        NetworkServer.SpawnObjects();
    }
}
