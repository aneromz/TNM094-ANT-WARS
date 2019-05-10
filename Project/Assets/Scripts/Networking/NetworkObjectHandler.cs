using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectHandler : NetworkBehaviour
{
    [Client]
    public void TellServerToDestroyAnt(GameObject ant)
    {
        CmdDestroyAnt(ant);
    }

    [Command]
    private void CmdDestroyAnt(GameObject ant)
    {
        ant.GetComponentInChildren<AgentControl>().CmdDestroyAnt();
        //NetworkServer.Destroy(ant);
    }
}
