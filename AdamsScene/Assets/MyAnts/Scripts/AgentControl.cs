using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Networking;

public class AgentControl : MonoBehaviour {


	public Transform home;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();

	}

	void Update (){
		agent.SetDestination(home.position);
	}


	void OnMouseDown(){
		Destroy (gameObject);
	}



}
