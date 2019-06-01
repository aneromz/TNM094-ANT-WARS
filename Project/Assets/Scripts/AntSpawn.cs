using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawn : MonoBehaviour
{

    public string team;
    private HeadUpDisplay hud;

    private void Awake()
    {
        hud = FindObjectOfType<HeadUpDisplay>();
    }

    void OnMouseDown()
    {
        if (team == PlayerPrefs.GetString("team"))
        {
            if (tag == "Left")
                hud.SpawnAntOnPosition1();
            else if (tag == "Middle")
                hud.SpawnAntOnPosition2();
            else if (tag == "Right")
                hud.SpawnAntOnPosition3();
        }
    }
}
