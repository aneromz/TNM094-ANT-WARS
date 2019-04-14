using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform leftSpawn;
    [SerializeField]
    private Transform middleSpawn;
    [SerializeField]
    private Transform rightSpawn;

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
        FindObjectOfType<PlayerObject>().CmdSpawnAnt(leftSpawn.position, leftSpawn.rotation);
    }

    private void SpawnAntOnPosition2()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnAnt(middleSpawn.position, middleSpawn.rotation);
    }

    private void SpawnAntOnPosition3()
    {
        FindObjectOfType<PlayerObject>().CmdSpawnAnt(rightSpawn.position, rightSpawn.rotation);
    }
}
