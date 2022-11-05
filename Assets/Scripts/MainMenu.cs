using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Story1");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart(){
        SceneManager.LoadScene("MainGame");
    }

    public void TrollScene(){
        SceneManager.LoadScene("TrollText");
    }

}
