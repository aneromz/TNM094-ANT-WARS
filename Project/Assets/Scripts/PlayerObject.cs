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

    void Update () {
        /*
		if (isLocalPlayer == false) {
			return;
		}
        
        */
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

        NetworkServer.SpawnWithClientAuthority(ant, connectionToClient);
	}

    [ClientRpc]
    public void RpcLoadGameScene()
    {
        GameObject.Find("Lobby").SetActive(false);
        GameObject.Find("Menu Background").SetActive(false);
        SceneManager.LoadScene(1);
    }
}
