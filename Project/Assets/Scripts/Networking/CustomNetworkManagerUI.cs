using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomNetworkManagerUI : MonoBehaviour
{
    [SerializeField]
    private NetworkManager manager;
    [SerializeField]
    private CustomNetworkDiscovery discovery;

    [SerializeField]
    public bool showGUI = true;
    [SerializeField]
    public int offsetX = 0;
    [SerializeField]
    public int offsetY = 0;

    // Menu stuff
    [SerializeField]
    private Button hostButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Image gameBrowser;
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private HeadUpDisplay hud;

    private bool menuIsVisible;

    private void Awake()
    {
        menuIsVisible = true;
        hostButton.onClick.AddListener(HostGame);
        exitButton.onClick.AddListener(ExitGame);
        exitButton.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
        menu.gameObject.SetActive(menuIsVisible);
    }

    public void HostGame ()
    {
        manager.StartHost();
        discovery.StartBroadcast();

        // Manage buttons
        hostButton.gameObject.SetActive(false);
        gameBrowser.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        hud.gameObject.SetActive(true);

        ToggleMenu();
    }

    public void ExitGame()
    {
        manager.StopHost();
        discovery.SearchForBroadcast();

        // Manage buttons
        hostButton.gameObject.SetActive(true);
        gameBrowser.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
    }

    public void JoinGame(LanBroadcastInfo gameInfo)
    {
        manager.networkAddress = gameInfo.ipAddress;
        manager.networkPort = gameInfo.port;
        manager.StartClient();

        // Manage buttons
        hostButton.gameObject.SetActive(false);
        gameBrowser.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        hud.gameObject.SetActive(true);

        ToggleMenu();

        discovery.StopBroadcast();
    }

    public void ToggleMenu()
    {
        menuIsVisible = menuIsVisible ? false : true;

        menu.gameObject.SetActive(menuIsVisible);
    }

    void OnGUI()
    {
        if (!showGUI)
            return;

        int xpos = 10 + offsetX;
        int ypos = 40 + offsetY;
        int spacing = 24;

        if (NetworkClient.active)
        {
            GUI.Label(new Rect(xpos, ypos, 300, 20), "Address: " + manager.networkAddress + " port: " + manager.networkPort);
            ypos += spacing;
        }

        if (NetworkClient.active && !ClientScene.ready)
        {
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Client Ready"))
            {
                ClientScene.Ready(manager.client.connection);

                if (ClientScene.localPlayers.Count == 0)
                {
                    ClientScene.AddPlayer(0);
                }
            }
            ypos += spacing;
        }
    }
}