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
    private Button playGameButton;
    [SerializeField]
    private Button helpButton;
    [SerializeField]
    private Button backHelpButton;
    [SerializeField]
    private Button backOptionsButton;
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
    [SerializeField]
    private Button playAgainButton;

    // Menu panels
    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject helpPanel;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    public GameObject lobbyPanel;
    [SerializeField]
    private GameObject inGameMenuPanel;
    [SerializeField]
    private GameObject menuBackground;
    [SerializeField]
    private GameObject gameOverPanel;

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
        playGameButton.onClick.AddListener(playGame);
        helpButton.onClick.AddListener(helpPage);
        backHelpButton.onClick.AddListener(startMenu);
        backOptionsButton.onClick.AddListener(startMenu);
        hostLobbyButton.onClick.AddListener(HostGame);
        exitGameButton.onClick.AddListener(ExitGame);
        findLobbyButton.onClick.AddListener(ToggleGameSearch);
        exitLobbyButton.onClick.AddListener(ExitLobby);
        startGameButton.onClick.AddListener(CmdStartGame);
        stopSearchButton.onClick.AddListener(ToggleGameSearch);
        playAgainButton.onClick.AddListener(playGame);

        // Deactivate in game menu
        menuIsVisible = false;
        inGameMenuPanel.SetActive(menuIsVisible);

        // Arrange panel visibility at game start
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        helpPanel.SetActive(false);
        optionsPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        stopSearchButton.gameObject.SetActive(false);
        isSearchingForGame = false;
    }

    [Command]
    private void CmdStartGame()
    {
        discovery.StopBroadcast();
        FindObjectOfType<PlayerObject>().RpcLoadGameScene();
    }

    private void playGame()
    {
        optionsPanel.SetActive(true);
        startPanel.SetActive(false);

        gameOverPanel.SetActive(false);
        menuBackground.SetActive(true);
        //NetworkManager.singleton.ServerChangeScene("StartMenu");
    }
    private void helpPage()
    {
        helpPanel.SetActive(true);
        startPanel.SetActive(false);
    }
    private void startMenu()
    {
        startPanel.SetActive(true);
        helpPanel.SetActive(false);
        optionsPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        //NetworkManager.singleton.ServerChangeScene("StartMenu");
    }

    public void HostGame ()
    {
        manager.StartHost(null, 4);
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
        gameOverPanel.SetActive(false);

        // Deactivate in game menu
        menuIsVisible = false;
        inGameMenuPanel.SetActive(menuIsVisible);

        // Activate options panel
        optionsPanel.SetActive(true);

        // Load start menu scene
        //SceneManager.LoadScene("StartMenu");
        NetworkManager.singleton.ServerChangeScene("StartMenu");

        manager.StopHost();
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
    }

    public void ToggleGameSearch()
    {
        // Switch
        isSearchingForGame = !isSearchingForGame;
        discovery.JoinGameOnRecievedBroadcast(isSearchingForGame);

        stopSearchButton.gameObject.SetActive(isSearchingForGame);
        findLobbyButton.gameObject.SetActive(!isSearchingForGame);
        hostLobbyButton.gameObject.SetActive(!isSearchingForGame);
        backOptionsButton.gameObject.SetActive(!isSearchingForGame);
        /*
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
        */
    }

    public void ToggleMenu()
    {
        menuIsVisible = menuIsVisible ? false : true;
        inGameMenuPanel.SetActive(menuIsVisible);
    }

    public void ShowGameOverPanel(string team)
    {
        FindObjectOfType<HeadUpDisplay>().DeactivateAllButtons();
        gameOverPanel.SetActive(true);

        Text gameOverMessage = gameOverPanel.GetComponentInChildren<Text>();
        if (team == "BlueHome")
            gameOverMessage.text = "Blue Team Won!";
        else
            gameOverMessage.text = "Red Team Won!";

    }
}