using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {
	
	public GameObject ant;
	private float timer;
	public float spawnTime;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > spawnTime)
		{
			timer = 0.0f;
			Instantiate(ant, transform.position, transform.localRotation);
			Instantiate(ant, transform.position, transform.localRotation);

		}
	}
}