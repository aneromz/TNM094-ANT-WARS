using UnityEngine;

public class AntHomeCollider : MonoBehaviour
{
    private float health = 100f;

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
    }
}