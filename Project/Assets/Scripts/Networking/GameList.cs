using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Networking;
using System;

public class GameList : MonoBehaviour
{

    [SerializeField]
    private JoinGameButton joinButtonPrefab;

    private void Awake()
    {
        // Callback every time the game list change
        AvailableGamesList.OnAvailableGamesListChanged += AvailableGamesList_OnAvailableGamesListChanged;
    }

    // Update game list
    private void AvailableGamesList_OnAvailableGamesListChanged(List<LanBroadcastInfo> games)
    {
        ClearButtons();
        CreateNewButtons(games);
    }

    private void ClearButtons()
    {
        var buttons = GetComponentsInChildren<JoinGameButton>();

        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }

    private void CreateNewButtons(List<LanBroadcastInfo> games)
    {
        foreach(var gameInfo in games)
        {
            var button = Instantiate(joinButtonPrefab);
            button.Initialize(gameInfo, transform);
        }
    }
}
