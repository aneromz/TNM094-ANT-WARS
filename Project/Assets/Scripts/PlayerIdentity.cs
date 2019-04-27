using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdentity : NetworkBehaviour
{
    [SyncVar] public string uniqueIdentity;
    private NetworkInstanceId netIdentity;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();

        // Every time a new player connects, refresh player identities
        CmdRefreshPlayerIdentities();
    }

    [Command]
    void CmdRefreshPlayerIdentities()
    {
        // Call on all clients
        RpcSetIdentities();
    }

    [ClientRpc]
    void RpcSetIdentities()
    {
        var players = FindObjectsOfType<PlayerIdentity>();
        foreach (var player in players)
        {
            if (player.transform.name == "PlayerObject(Clone)")
            {
                player.SetIdentity();
            }
        }
    }

    [Client]
    void GetNetIdentity()
    {
        netIdentity = GetComponent<NetworkIdentity>().netId;
        CmdGiveServerUniqueIdentity("Player " + netIdentity.ToString());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            transform.name = uniqueIdentity;
        }
        else
        {
            transform.name = CreateUniqueIdentity();
        }
    }

    private string CreateUniqueIdentity()
    {
        return "Player " + netIdentity.ToString();
    }

    [Command]
    void CmdGiveServerUniqueIdentity(string identity)
    {
        uniqueIdentity = identity;
    }

}
