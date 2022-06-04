using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] GameObject controlMenu;
    [SerializeField] GameObject pauseMenu;
    
    public void enter()
    {
        controlMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void exit()
    {
        controlMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
