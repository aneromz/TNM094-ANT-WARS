using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	public GameObject antPrefab;
    public Transform leftSpawn;
    public Transform middleSpawn;
    public Transform rightSpawn;

	
	void Update () {

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
    }

	[Command]
	void CmdSpawnMyAnt(int key)
	{
		GameObject ant;

        switch(key)
        {
            case 1:
                ant = Instantiate(antPrefab, leftSpawn.position, leftSpawn.rotation);
                break;
            case 2:
                ant = Instantiate(antPrefab, middleSpawn.position, middleSpawn.rotation);
                break;
            case 3:
                ant = Instantiate(antPrefab, rightSpawn.position, rightSpawn.rotation);
                break;
            default:
                ant = Instantiate(antPrefab);
                break;
        }

		NetworkServer.SpawnWithClientAuthority(ant, connectionToClient);
	}

    [Command]
    public void CmdSpawnAnt(Vector3 position, Quaternion rotation)
    {
        GameObject ant = Instantiate(antPrefab, position, rotation);

        NetworkServer.SpawnWithClientAuthority(ant, connectionToClient);
    }


}
