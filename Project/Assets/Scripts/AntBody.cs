using UnityEngine;

public class AntBody : MonoBehaviour
{
    void OnMouseDown()
    {
        GetComponentInParent<AgentControl>().CmdDestroyAnt();
    }
}
