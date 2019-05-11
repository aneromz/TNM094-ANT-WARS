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
    }

    [Client]
    public void TellServerToDamageHome(GameObject home)
    {
        CmdDamageHome(home);
    }

    [Command]
    private void CmdDamageHome(GameObject home)
    {
        home.GetComponentInChildren<AntHomeNetwork>().CmdTakeDamage();
    }
}
