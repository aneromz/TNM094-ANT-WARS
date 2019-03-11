using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameKeyboardEvent : MonoBehaviour {

    private GameObject menu;

    void Start ()
    {
        menu = GameObject.Find("Main Menu");
        menu.SetActive(false);
    }

	void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (menu.activeInHierarchy == false)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
