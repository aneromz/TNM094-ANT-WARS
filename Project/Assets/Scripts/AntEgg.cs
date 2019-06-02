using UnityEngine;
using UnityEngine.Networking;

public class AntEgg : NetworkBehaviour
{
    public int value;
    private Vector3 updatedRotation;

    void Start()
    {
        updatedRotation = Vector3.zero;
        transform.parent = GameObject.Find("Map Content").transform;
    }

    void Update()
    {
        RotateObject();
    }

    private void OnMouseDown()
    {
        GameObject.Find(PlayerPrefs.GetString("uniqueIdentity")).GetComponentInChildren<NetworkObjectHandler>().TellServerToDestroyAntEgg(gameObject);
        FindObjectOfType<HeadUpDisplay>().IncreaseResources(value);
    }

    private void RotateObject()
    {
        updatedRotation.z = 20f * Time.deltaTime;
        transform.Rotate(updatedRotation);
    }

    [Command]
    public void CmdDestroyEgg()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
