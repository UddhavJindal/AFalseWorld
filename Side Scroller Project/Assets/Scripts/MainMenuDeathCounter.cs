using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuDeathCounter : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    private void Start()
    {
        text.text = PlayerPrefs.GetInt("DeathCounter").ToString();
    }
}
