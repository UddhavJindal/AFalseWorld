using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator modeAnim;
    public Animator settingsAnim;

    public void ModeBTN()
    {
        modeAnim.SetBool("modeClick", true);
    }

    public void SettingsBTN()
    {
        settingsAnim.SetBool("settingsClick", true);
    }

    public void ModeToMenu()
    {
        modeAnim.SetBool("modeClick", false);
    }

    public void SettingsToMenu()
    {
        settingsAnim.SetBool("settingsClick", false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
