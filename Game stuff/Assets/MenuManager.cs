using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject Panel_MoreInfo;
    public GameObject Panel_Credits;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void OpenControls_MoreInfo()
    {
        if (Panel_MoreInfo != null)
        {
            bool isActive = Panel_MoreInfo.activeSelf;

            Panel_MoreInfo.SetActive(!isActive);
        }
    }
    public void OpenControls_Credits()
    {
        if (Panel_Credits != null)
        {
            bool isActive = Panel_Credits.activeSelf;

            Panel_Credits.SetActive(!isActive);
        }
    }
}
