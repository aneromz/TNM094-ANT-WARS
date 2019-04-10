using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnOnClick : MonoBehaviour {

	public GameObject ant;
		
	void OnMouseDown()
		{
			Instantiate (ant, transform.position, transform.rotation);
		}

}
