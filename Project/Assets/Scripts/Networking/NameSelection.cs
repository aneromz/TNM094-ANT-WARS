using UnityEngine;
using UnityEngine.UI;

public class NameSelection : MonoBehaviour
{
    private string playerName;
    public InputField mainInputField;
    public bool isEmpty = false;
    public Button playGameButton; 
    void Awake()
    {
        if (PlayerPrefs.HasKey("playerName"))
            GetComponentInChildren<InputField>().text = PlayerPrefs.GetString("playerName");

       

        mainInputField.onValueChanged.AddListener(delegate { LockInput(); });
        isEmpty = !(GetComponentInChildren<InputField>().text.Length > 0);
        playGameButton.interactable = !(isEmpty);
        playGameButton.onClick.AddListener(SetPlayerName);
    }


    public void SetPlayerName()
    {
        playerName = GetComponentInChildren<InputField>().text;

        if (playerName == "")
            playerName = "No Name";

        PlayerPrefs.SetString("playerName", playerName);
    }
    public void LockInput()
    {
        
        isEmpty = !(GetComponentInChildren<InputField>().text.Length > 0);
        playGameButton.interactable = !(isEmpty);      
    }
}
