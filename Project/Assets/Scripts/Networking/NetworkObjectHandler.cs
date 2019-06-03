using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectHandler : NetworkBehaviour
{

    [SerializeField]
    private GameObject antEggPrefab;

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

    [Client]
    public void TellServerToSpawnAntEgg(GameObject ant)
    {
        CmdSpawnAntEgg(ant);
    }

    [Command]
    private void CmdSpawnAntEgg(GameObject ant)
    {
        GameObject egg = Instantiate(antEggPrefab);
        egg.transform.position = ant.transform.position;
        
        NetworkServer.SpawnWithClientAuthority(egg, connectionToClient);
    }

    [Client]
    public void TellServerToDestroyAntEgg(GameObject egg)
    {
        CmdDestroyAntEgg(egg);
    }

    [Command]
    private void CmdDestroyAntEgg(GameObject egg)
    {
        egg.GetComponentInChildren<AntEgg>().CmdDestroyEgg();
    }
}
