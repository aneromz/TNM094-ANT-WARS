using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{

    private float startHealth = 100f;
    private float health; 


    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {

        health = startHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision Collision)
    { 

        if(Collision.gameObject.tag == "BlueAnt")
        {
            if (startHealth <= 0)
            {
                Debug.Log("you lost!");
                gameObject.SetActive(false);
            }
            TakeDamage();
        }


        

    }

    void TakeDamage()
    {
        startHealth -= 0.1f;
        Debug.Log(startHealth);

        healthBar.fillAmount = health / startHealth;

    }

}
