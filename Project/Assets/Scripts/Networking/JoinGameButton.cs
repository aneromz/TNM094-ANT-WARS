using UnityEngine;
using UnityEngine.UI;

public class JoinGameButton : MonoBehaviour
{
    private Text buttonName;
    private LanBroadcastInfo gameInfo;

    private void Awake ()
    {
        buttonName = GetComponentInChildren<Text>();
        GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    public void Initialize(LanBroadcastInfo gameInfo, Transform gameListTransform)
    {
        this.gameInfo = gameInfo;
        buttonName.text = gameInfo.broadcastName;

        transform.SetParent(gameListTransform);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    private void JoinGame()
    {
        FindObjectOfType<CustomNetworkManagerUI>().JoinLobby(gameInfo);
    }
}
