using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{
    private float health = 100f;


    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision Collision)
    { 

        if(Collision.gameObject.tag == "BlueAnt")
        {
            if (health <= 0)
            {
                Debug.Log("you lost!");
                gameObject.SetActive(false);
            }
            TakeDamage();
        }


        

    }

    void TakeDamage()
    {
        health -= 0.1f;
        Debug.Log(health);

      //  healthBar.fillAmount 

    }

}
