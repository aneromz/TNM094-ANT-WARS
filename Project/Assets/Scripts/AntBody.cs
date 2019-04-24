using UnityEngine;
using UnityEngine.UI;

public class AntBody : MonoBehaviour
{
    public float health = 100f;
    public Image healthBar;


    void OnMouseDown()
    {
        TakeDamage();
       // GetComponentInParent<AgentControl>().CmdDestroyAnt();
    }

    public void TakeDamage()
    {

        health -= 40f;
        healthBar.fillAmount = health / 100f;

        if (health < 0.1f)
        {
            GetComponentInParent<AgentControl>().CmdDestroyAnt();
        }

       
    }

}
