using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
{
    [SerializeField]
    private Button spawnButton1;
    [SerializeField]
    private Button spawnButton2;
    [SerializeField]
    private Button spawnButton3;
    [SerializeField]
    private Button menuButton;

    private bool menuIsVisible;

    private Toggle teamToggle;

    private bool blue = true;

    public void Start ()
    {
        menuIsVisible = true;
        spawnButton1.onClick.AddListener(SpawnAntOnPosition1);
        spawnButton2.onClick.AddListener(SpawnAntOnPosition2);
        spawnButton3.onClick.AddListener(SpawnAntOnPosition3);
        menuButton.onClick.AddListener(ToggleMenu);
        teamToggle = GetComponentInChildren<Toggle>();

        teamToggle.onValueChanged.AddListener(delegate {
            ChangeTeam(teamToggle);
        });
    }

    private void SpawnAntOnPosition1 ()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnMyAnt(1, blue);
    }

    private void SpawnAntOnPosition2()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnMyAnt(2, blue);
    }

    private void SpawnAntOnPosition3()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnMyAnt(3, blue);
    }

    private void ToggleMenu()
    {
        ToggleButtonVisibility();

        FindObjectOfType<CustomNetworkManagerUI>().ToggleMenu();
    }

    private void OnDisable()
    {
        ToggleButtonVisibility();
    }

    private void ToggleButtonVisibility ()
    {
        menuIsVisible = menuIsVisible ? false : true;
        spawnButton1.gameObject.SetActive(menuIsVisible);
        spawnButton2.gameObject.SetActive(menuIsVisible);
        spawnButton3.gameObject.SetActive(menuIsVisible);
    }

    private void ChangeTeam(Toggle change)
    {
        blue = !blue;
    }
}
