using UnityEngine;
using UnityEngine.UI;

public class NameSelection : MonoBehaviour
{
    private string playerName;

    void Awake()
    {
        if (PlayerPrefs.HasKey("playerName"))
            GetComponentInChildren<InputField>().text = PlayerPrefs.GetString("playerName");

        GetComponentInChildren<Button>().onClick.AddListener(SetPlayerName);
    }

    public void SetPlayerName()
    {
        playerName = GetComponentInChildren<InputField>().text;

        if (playerName == "")
            playerName = "No Name";

        PlayerPrefs.SetString("playerName", playerName);
    }
}
