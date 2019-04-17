using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AgentControl : NetworkBehaviour {


	NavMeshAgent agent;
    private Transform home;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();

        string homeTag = "";

        if (this.tag == "BlueAnt")
            homeTag = "BlueHome";
        else if (this.tag == "RedAnt")
            homeTag = "RedHome";

        home = GameObject.FindWithTag(homeTag).transform;
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
