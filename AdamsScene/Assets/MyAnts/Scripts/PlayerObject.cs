using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	// Use this for initialization
	void Start () {

		if (isLocalPlayer == false) {
			return;
		}

		CmdSpawnMyAnt();

	}

	public GameObject PlayerAntPrefab;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			CmdSpawnMyAnt ();
		}

		
	}

	[Command]
	void CmdSpawnMyAnt()
	{
		GameObject ant = Instantiate (PlayerAntPrefab);
		NetworkServer.SpawnWithClientAuthority(ant, connectionToClient);

	}
}
