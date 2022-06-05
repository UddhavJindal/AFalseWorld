using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UStartingText : MonoBehaviour
{
    [SerializeField] AudioClip[] souldClips;

    private void Start()
    {
        if(souldClips.Length != 0)
        {
            GameObject.FindGameObjectWithTag("DMSource").GetComponent<AudioSource>().clip = souldClips[Random.Range(0, souldClips.Length)];
            GameObject.FindGameObjectWithTag("DMSource").GetComponent<AudioSource>().Play();
        }
    }
}
