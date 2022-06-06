using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuName : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    private void Start()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")) || string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
        {
            text.text = "LOSER";
            
        }
        else
        {
            text.text = PlayerPrefs.GetString("PlayerName");
        }
    }
}
