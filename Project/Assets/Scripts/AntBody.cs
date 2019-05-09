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
        GameObject.Find(PlayerPrefs.GetString("uniqueIdentity")).GetComponentInChildren<NetworkAntDestroyer>().TellServerToDestroyAnt(ant.gameObject);

        //TakeDamage();
        // GetComponentInParent<AgentControl>().CmdDestroyAnt();
    }

    public void TakeDamage()
    {
        health -= 100f;
        healthBar.fillAmount = health / 100f;

        if (health < 0.1f)
        {
            GetComponentInParent<AgentControl>().CmdDestroyAnt();
        }
    }
}
