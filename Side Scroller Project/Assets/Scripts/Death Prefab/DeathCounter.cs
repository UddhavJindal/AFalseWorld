using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] float Speed;

    [SerializeField] AudioClip[] souldClips;

    float currentAlphaValue;

    int noOfDeaths;

    private void Start()
    {
        text.alpha = 1;
        currentAlphaValue = text.alpha;
        noOfDeaths = PlayerPrefs.GetInt("DeathCounter");
        text.text = noOfDeaths.ToString();
        GameObject.FindGameObjectWithTag("DMSource").GetComponent<AudioSource>().clip = souldClips[Random.Range(0, souldClips.Length)];
        GameObject.FindGameObjectWithTag("DMSource").GetComponent<AudioSource>().Play();
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
