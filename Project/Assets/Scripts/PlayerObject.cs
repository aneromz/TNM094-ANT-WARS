using UnityEngine;
using UnityEngine.Networking;

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
        
		if (Input.GetKeyDown(KeyCode.Q))
        {
			CmdSpawnMyAnt(1);
		}

        if (Input.GetKeyDown(KeyCode.W))
        {
            CmdSpawnMyAnt(2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdSpawnMyAnt(3);
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
}
