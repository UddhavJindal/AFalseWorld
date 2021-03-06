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
    [SerializeField] AudioClip pauseMenuSound;
    [SerializeField] AudioSource audioSource;
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
            if(pauseMenuSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pauseMenuSound);
            }
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

    public void SceneChanger(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
