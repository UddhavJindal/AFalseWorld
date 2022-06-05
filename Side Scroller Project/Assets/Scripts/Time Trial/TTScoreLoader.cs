using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TTScoreLoader : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    [SerializeField] string SaveFileName;

    private void Start()
    {
        if(PlayerPrefs.GetFloat(SaveFileName).ToString("n2") != null)
        {
            text.text = PlayerPrefs.GetFloat(SaveFileName).ToString("n2");
        }
        else
        {
            text.text = "Loser";
        }
    }
}
