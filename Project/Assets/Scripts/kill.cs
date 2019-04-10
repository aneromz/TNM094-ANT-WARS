using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class kill : NetworkBehaviour {


	void OnMouseDown(){
		Destroy(gameObject);
	}

}
