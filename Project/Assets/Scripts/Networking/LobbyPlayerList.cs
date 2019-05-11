using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class LobbyPlayerList : NetworkBehaviour
{
    [SerializeField]
    private Text namePrefab;

    private void Awake()
    {
        //StartCoroutine(RefreshPlayerList());
    }

    private void OnEnable()
    {
        StartCoroutine(RefreshPlayerList());
    }

    private IEnumerator RefreshPlayerList()
    {
        UpdatePlayerList();
        yield return new WaitForSeconds(0.5f);
        yield return RefreshPlayerList();
    }

    public void UpdatePlayerList()
    {
        CleanList();
        CreateList();
    }

    private void CleanList()
    {
        var playerNameList = GetComponentsInChildren<Text>();

        foreach (var playerName in playerNameList)
        {
            Destroy(playerName.gameObject);
        }
    }

    private void CreateList()
    {
        var players = FindObjectsOfType<PlayerIdentity>();

        for (int i = players.Length; i > 0; --i)
        {
            Text nameText = Instantiate(namePrefab, transform);
            nameText.text = players[i - 1].playerName;

            if (players[i - 1].uniqueIdentity == PlayerPrefs.GetString("uniqueIdentity"))
            {
                nameText.fontStyle = FontStyle.Bold;
                nameText.color = Color.black;
            }
                
        }
    }
}
