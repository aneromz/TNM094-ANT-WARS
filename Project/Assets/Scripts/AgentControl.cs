using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class AgentControl : NetworkBehaviour
{ 
    private NavMeshAgent agent;
    private Transform home;

    private Transform sceneTransform;
    private Quaternion rotationOffset;

    private AntBody antBody;
    float timeLeft = 3;
    bool AntIsDead = false;


    // Use this for initialization
    void Start ()
    {
        // The ants navigation system used to calculate a path from the navmesh
		agent = GetComponent<NavMeshAgent> ();
        // Object that contains the 3D model and the collider of the ant body
        antBody = GetComponentInChildren<AntBody>();

        // The scenes transform used to simulate a parent transform
        sceneTransform = GameObject.Find("Map Content").transform;

        // Rotate the ant body to correct position
        rotationOffset = Quaternion.Euler(Mathf.PI / 2, 0, 0);

        if (tag == "BlueAnt")
            home = GameObject.FindWithTag("BlueHome").transform;
        else
            home = GameObject.FindWithTag("RedHome").transform;
    }

	void Update ()
    {
        // Calculate path
		agent.SetDestination(home.position);

        // Set the position and rotation of the visible ant body
        antBody.transform.position = sceneTransform.TransformPoint(transform.position);
        antBody.transform.rotation = sceneTransform.rotation * transform.rotation * rotationOffset;

        if (AntIsDead)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                Destroy(gameObject);
            }
        }


    }

    [Command]
	public void CmdDestroyAnt()
    {
        if (!AntIsDead)
        {
            SoundManager.PlaySound();
            antBody.GetComponent<BoxCollider>().enabled = false;
            antBody.transform.GetChild(0).localScale += new Vector3(10, 0, -5);
            agent.enabled = false;
            AntIsDead = true;
        }
    }
}