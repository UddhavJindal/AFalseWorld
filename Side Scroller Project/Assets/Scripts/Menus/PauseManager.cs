using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    #region Variables
    [Header("References")]
    public GameObject pauseMenu;
    public KeyCode esc;
    public KeyCode pKey;
    public bool canPause;
    #endregion

    #region Pre-Defined Variables
    private void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (canPause && (Input.GetKeyDown(esc) || Input.GetKeyDown(pKey)))
        {
            PauseMechanics();
        }
    }
    #endregion

    #region Custom Functions
    void PauseMechanics()
    {
        if(pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SceneChanger(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
