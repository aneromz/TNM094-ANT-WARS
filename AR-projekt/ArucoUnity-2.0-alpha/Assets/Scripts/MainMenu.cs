using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void LoadCameraCalibrationScene ()
    {
        SceneManager.LoadScene("CalibrateCamera");
    }

    public void LoadTrackMarkersScene()
    {
        Debug.Log("TrackMarkers");
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }
}
