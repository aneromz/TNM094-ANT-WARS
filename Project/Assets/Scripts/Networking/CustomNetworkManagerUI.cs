using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNetworkManagerUI : NetworkBehaviour
{
    [SerializeField]
    private CustomNetworkManager manager;
    [SerializeField]
    private CustomNetworkDiscovery discovery;

    // Menu buttons
    [SerializeField]
    private Button hostLobbyButton;
    [SerializeField]
    private Button exitGameButton;
    [SerializeField]
    private Button exitLobbyButton;
    [SerializeField]
    private Button findLobbyButton;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button stopSearchButton;
    
    // Menu panels
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject nameSelectionPanel;
    [SerializeField]
    public GameObject lobbyPanel;
    [SerializeField]
    private GameObject inGameMenuPanel;
    [SerializeField]
    private GameObject menuBackground;

    private bool menuIsVisible;
    private bool isSearchingForGame;

    public static CustomNetworkManagerUI instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        // Add onclick functions to buttons
        hostLobbyButton.onClick.AddListener(HostGame);
        exitGameButton.onClick.AddListener(ExitGame);
        findLobbyButton.onClick.AddListener(ToggleGameSearch);
        exitLobbyButton.onClick.AddListener(ExitLobby);
        startGameButton.onClick.AddListener(CmdStartGame);
        stopSearchButton.onClick.AddListener(ToggleGameSearch);

        // Deactivate in game menu
        menuIsVisible = false;
        inGameMenuPanel.SetActive(menuIsVisible);

        // Arrange panel visibility
        nameSelectionPanel.SetActive(true);
        nameSelectionPanel.GetComponentInChildren<Button>().onClick.AddListener(SelectName);
        optionsPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        stopSearchButton.gameObject.SetActive(false);

        isSearchingForGame = false;
    }
    
    [Command]
    private void CmdStartGame()
    {
        FindObjectOfType<PlayerObject>().RpcLoadGameScene();
    }

    public void SelectName()
    {
        nameSelectionPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void HostGame ()
    {
        manager.StartHost();
        discovery.StartBroadcast();

        optionsPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        startGameButton.gameObject.SetActive(true);
    }

    public void ExitLobby()
    {
        lobbyPanel.SetActive(false);
        optionsPanel.SetActive(true);

        manager.StopHost();
        discovery.StopBroadcast();
        discovery.Reset();
    }

    public void ExitGame()
    {
        menuBackground.SetActive(true);

        // Deactivate in game menu
        ToggleMenu();

        // Activate options panel
        optionsPanel.SetActive(true);

        // Load start menu scene
        SceneManager.LoadScene("StartMenu");

        manager.StopHost();
        discovery.StopBroadcast();
        discovery.Reset();
    }

    public void JoinLobby(LanBroadcastInfo gameInfo)
    {
        // Configure network settings
        manager.networkAddress = gameInfo.ipAddress;
        manager.networkPort = gameInfo.port;
        manager.StartClient();

        // Rearrange menu visibility
        optionsPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        stopSearchButton.gameObject.SetActive(false);
        findLobbyButton.gameObject.SetActive(true);
        hostLobbyButton.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(false);

        // Stop listen for broadcasts
        discovery.StopBroadcast(); 
        discovery.CleanUpCoroutines();
    }

    public void ToggleGameSearch()
    {
        // Switch
        isSearchingForGame = !isSearchingForGame;

        if (isSearchingForGame)
        {
            discovery.JoinGameOnRecievedBroadcast(true);

            stopSearchButton.gameObject.SetActive(true);
            findLobbyButton.gameObject.SetActive(false);
            hostLobbyButton.gameObject.SetActive(false);
        }
        else
        {
            discovery.JoinGameOnRecievedBroadcast(false);

            stopSearchButton.gameObject.SetActive(false);
            findLobbyButton.gameObject.SetActive(true);
            hostLobbyButton.gameObject.SetActive(true);
        }
    }

    public void ToggleMenu()
    {
        menuIsVisible = menuIsVisible ? false : true;
        inGameMenuPanel.SetActive(menuIsVisible);
    }
}