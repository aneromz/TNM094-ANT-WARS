using UnityEngine;

public class AntHomeCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected!");
    }
}