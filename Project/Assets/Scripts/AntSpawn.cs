using UnityEngine;

public class AntSpawn : MonoBehaviour
{

    public string team;
    private HeadUpDisplay hud;

    private float coolDown;

    private void Awake()
    {
        hud = FindObjectOfType<HeadUpDisplay>();
        coolDown = -1f;
    }

    private void Update()
    {

        if (coolDown > 0f)
            coolDown -= Time.deltaTime;
    }

    void OnMouseDown()
    {
        if (team == PlayerPrefs.GetString("team") && coolDown < 0f)
        {
            coolDown = 0.5f;
            // Give feedback
            Vibrator.Vibrate(50);

            if (tag == "Left")
                hud.SpawnAntOnPosition1();
            else if (tag == "Middle")
                hud.SpawnAntOnPosition2();
            else if (tag == "Right")
                hud.SpawnAntOnPosition3();
        }
    }
}
