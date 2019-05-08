using UnityEngine;
using UnityEngine.UI;

public class AntBody : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;


    void OnMouseDown()
    {
        SoundManager.PlaySound();
        GetComponentInParent<AgentControl>().CmdDestroyAnt();
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
