using UnityEngine;
using UnityEngine.UI;

public class AntHomeCollider : MonoBehaviour
{
    private float health = 100f;

    public Image healthBar;

    private float timer = 0;

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

    public void OnCollisionExit(Collision collision)
    {
        healthBar.color = Color.green;
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

        timer += Time.deltaTime;
            if (timer > 0.2f)
            {
                if (healthBar.color == Color.white)
                {
                    healthBar.color = Color.green;
                }
                else healthBar.color = Color.white;
                timer = 0;
            }

        }


}