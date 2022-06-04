using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] float Speed;

    float currentAlphaValue;

    int noOfDeaths;

    private void Start()
    {
        text.alpha = 1;
        currentAlphaValue = text.alpha;
        noOfDeaths = PlayerPrefs.GetInt("DeathCounter");
        text.text = noOfDeaths.ToString();
    }

    private void Update()
    {
        currentAlphaValue -= Time.deltaTime * Speed;
        text.alpha = currentAlphaValue;
        if(currentAlphaValue <= 0)
        {
            Destroy(gameObject);
        }
    }
}
