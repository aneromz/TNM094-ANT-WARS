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

    public void Start ()
    {
        spawnButton1.onClick.AddListener(SpawnAntOnPosition1);
        spawnButton2.onClick.AddListener(SpawnAntOnPosition2);
        spawnButton3.onClick.AddListener(SpawnAntOnPosition3);
    }

    private void SpawnAntOnPosition1 ()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnMyAnt(1);
    }

    private void SpawnAntOnPosition2()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnMyAnt(2);
    }

    private void SpawnAntOnPosition3()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnMyAnt(3);
    }
}
