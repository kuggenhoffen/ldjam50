using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void OnBtnExitClicked()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void OnBtnStartClicked()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
    }
}
