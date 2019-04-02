using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnOnClick : MonoBehaviour {

	public GameObject ant;
		
	void OnMouseDown()
		{
		GameObject ant2 = (GameObject)Instantiate (ant, transform.position, transform.rotation);
		}

}
