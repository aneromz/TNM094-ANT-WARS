using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AgentControl : NetworkBehaviour
{ 
    private NavMeshAgent agent;
    private Transform home;

    private Transform sceneTransform;
    private Quaternion rotationOffset;
    private Vector3 positionOffset;

    private AntBody antBody;

    // Use this for initialization
    void Start ()
    {
        // The ants navigation system used to calculate a path from the navmesh
		agent = GetComponent<NavMeshAgent> ();
        // Object that contains the 3D model and the collider of the ant body
        antBody = GetComponentInChildren<AntBody>();

        // The scenes transform used to simulate a parent transform
        sceneTransform = GameObject.Find("ImageTarget").transform;

        // Rotate the ant body to correct position
        rotationOffset = Quaternion.Euler(Mathf.PI / 2, 0, 0);
        // Correctly position the ant on the map
        positionOffset = GameObject.Find("SceneAnchor").transform.localPosition;

        string homeTag = "";
        if (tag == "BlueAnt")
            homeTag = "BlueHome";
        else if (tag == "RedAnt")
            homeTag = "RedHome";

        home = GameObject.FindWithTag(homeTag).transform;
    }

	void Update ()
    {
        // Calculate path
		agent.SetDestination(home.position);

        // Set the position and rotation of the visible ant body
        antBody.transform.position = sceneTransform.TransformPoint(transform.position + positionOffset);
        antBody.transform.rotation = sceneTransform.rotation * transform.rotation * rotationOffset;
    }

    [Command]
	public void CmdDestroyAnt()
    {
        Destroy (gameObject);
	}
}