using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    private GameObject DontDestroy;
        private GameObject Pause;

    public void PlayGame()
    {
   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Settings.SetActive(true);

    }
      public void Restart()
    {
     
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        Pause= GameObject.Find("Pause");
        Destroy(Pause);
        DontDestroy= GameObject.Find("DontDestroy");
        Destroy(DontDestroy);
        SceneManager.LoadScene(0);
        


    }
    public void PauseGame(){
        Time.timeScale=0;
    }
    public void ResumeGame(){
                Time.timeScale=1;

    }
   
}
