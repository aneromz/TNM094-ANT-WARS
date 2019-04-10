using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AgentControl : NetworkBehaviour {


	public Transform home;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();

	}

	void Update (){
		agent.SetDestination(home.position);

		if (hasAuthority == false) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdDestroyAnt ();
		}

	}
		
	void OnMouseDown(){
			CmdDestroyAnt ();
	}

	[Command]
	void CmdDestroyAnt(){
			Destroy (gameObject);
	}

}
