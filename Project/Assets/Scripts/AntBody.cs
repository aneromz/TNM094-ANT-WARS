using UnityEngine;
using UnityEngine.UI;

public class AntBody : MonoBehaviour
{
    void OnMouseDown()
    {
        SoundManager.PlaySound();

        AgentControl ant = GetComponentInParent<AgentControl>();
        GameObject.Find(PlayerPrefs.GetString("uniqueIdentity")).GetComponentInChildren<NetworkObjectHandler>().TellServerToDestroyAnt(ant.gameObject);
    }

}
