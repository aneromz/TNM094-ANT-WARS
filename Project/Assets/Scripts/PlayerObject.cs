using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerObject : NetworkBehaviour {

	public GameObject BlueAntPrefab;
    public GameObject RedAntPrefab;

    public Transform BlueLeftSpawn;
    public Transform BlueMiddleSpawn;
    public Transform BlueRightSpawn;

    public Transform RedLeftSpawn;
    public Transform RedMiddleSpawn;
    public Transform RedRightSpawn;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    [Command]
	public void CmdSpawnMyAnt(int key, bool blue)
	{
        GameObject ant;

        switch(key)
        {
            case 1: // Left Spawn
                if (blue)
                {
                    ant = Instantiate(BlueAntPrefab, BlueLeftSpawn.position, BlueLeftSpawn.rotation);
                }
                else
                {
                    ant = Instantiate(RedAntPrefab, RedLeftSpawn.position, RedLeftSpawn.rotation);
                }
                break;
            case 2: // Middle Spawn
                if (blue)
                {
                    ant = Instantiate(BlueAntPrefab, BlueMiddleSpawn.position, BlueMiddleSpawn.rotation);
                }
                else
                {
                    ant = Instantiate(RedAntPrefab, RedMiddleSpawn.position, RedMiddleSpawn.rotation);
                }
                break;
            case 3: // Right Spawn
                if (blue)
                {
                    ant = Instantiate(BlueAntPrefab, BlueRightSpawn.position, BlueRightSpawn.rotation);
                }
                else
                {
                    ant = Instantiate(RedAntPrefab, RedRightSpawn.position, RedRightSpawn.rotation);
                }
                break;
            default:
                return;
        }

        ant.GetComponentInChildren<AgentControl>().SetOwnerId(PlayerPrefs.GetString("uniqueIdentity"));
        NetworkServer.SpawnWithClientAuthority(ant, connectionToClient);
	}

    [ClientRpc]
    public void RpcLoadGameScene()
    {
        AssignTeams();

        GameObject.Find("Lobby").SetActive(false);
        GameObject.Find("Menu Background").SetActive(false);
        NetworkManager.singleton.ServerChangeScene("Game");
        //NetworkManager.singleton.ServerChangeScene("GameNoAR");
        //SceneManager.LoadScene(1);
    }

    private void AssignTeams()
    {
        var players = FindObjectsOfType<PlayerIdentity>();
        bool blue = false;
        for (int i = 0; i < players.Length; ++i)
        {
            if (players[i].uniqueIdentity == PlayerPrefs.GetString("uniqueIdentity"))
            {
                if (blue)
                    PlayerPrefs.SetInt("team", 0);
                else
                    PlayerPrefs.SetInt("team", 1);
            }
            blue = !blue;
        }
    }
}
