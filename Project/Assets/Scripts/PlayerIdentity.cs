using UnityEngine;
using UnityEngine.Networking;

public class PlayerIdentity : NetworkBehaviour
{
    [SyncVar] public string uniqueIdentity;
    [SyncVar] public string playerName;
    private NetworkInstanceId netIdentity;

    public override void OnStartLocalPlayer()
    {
        SetPlayerName();
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
            if (player.transform.name == "PlayerObject(Clone)" || player.transform.name == "")
            {
                player.SetIdentity();
            }
        }
    }

    [Client]
    void GetNetIdentity()
    {
        netIdentity = GetComponent<NetworkIdentity>().netId;
        PlayerPrefs.SetString("uniqueIdentity", CreateUniqueIdentity());
        CmdGiveServerUniqueIdentity(CreateUniqueIdentity());
    }

    public void SetIdentity()
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

    [Client]
    public void SetPlayerName()
    {
        CmdGiveServerPlayerName(PlayerPrefs.GetString("playerName"));
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

    [Command]
    void CmdGiveServerPlayerName(string name)
    {
        playerName = name;
    }
}
