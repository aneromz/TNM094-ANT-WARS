using UnityEngine;
using UnityEngine.Networking;

public class AntEgg : NetworkBehaviour
{
    public int value;
    private Vector3 updatedRotation;

    void Start()
    {
        updatedRotation = Vector3.zero;
        updatedRotation.x = 90;
        transform.parent = GameObject.Find("Map Content").transform;
        transform.localPosition += new Vector3(0f, 1f, 0f);
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
        updatedRotation.z = 25f * Time.time;

        transform.localRotation = Quaternion.Euler(updatedRotation);
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
