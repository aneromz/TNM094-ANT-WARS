using UnityEngine;
using UnityEngine.UI;

public class AntBody : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;

    void OnMouseDown()
    {
        SoundManager.PlaySound();

        AgentControl ant = GetComponentInParent<AgentControl>();
        GameObject.Find(PlayerPrefs.GetString("uniqueIdentity")).GetComponentInChildren<NetworkObjectHandler>().TellServerToDestroyAnt(ant.gameObject);
    }

}
