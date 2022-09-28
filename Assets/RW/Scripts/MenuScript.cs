using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour{
    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void OptionsMenu(){
        SceneManager.LoadScene("OptionsMenu");
    }

    public void BackMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Instructions(){
        SceneManager.LoadScene("InstructionsMenu");
    }

}
