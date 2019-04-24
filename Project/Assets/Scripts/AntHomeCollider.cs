using UnityEngine;
using UnityEngine.UI;

public class AntHomeCollider : MonoBehaviour
{
    private float health = 100f;

    public Image healthBar;

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "RedAnt" && tag == "RedHome")
        {
            TakeDamage();
        }
        else if (collision.gameObject.tag == "BlueAnt" && tag == "BlueHome")
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {

        if (health < 0.1f)
        {
            Debug.Log(tag + " is dead!");
            gameObject.SetActive(false);
            return;
        }
        health -= 0.1f;
        Debug.Log(tag + " Health: " + health);
        healthBar.fillAmount = health / 100f;

        if(health < 80f)
        {
            if( (health % 3) == 0)
            healthBar.color = Color.white;
            else
            {
              healthBar.color = Color.green;
            }
        }
        

    }
}