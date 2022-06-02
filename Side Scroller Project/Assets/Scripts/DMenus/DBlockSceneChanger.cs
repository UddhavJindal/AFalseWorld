using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DBlockSceneChanger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer card;

    [Header("Color")]
    [SerializeField] Color32 idleColor;
    [SerializeField] Color32 activeColor;

    [Header("Settings")]
    [SerializeField] string sceneName;

    //flag
    bool canChangeScene;

    private void Start()
    {
        card.color = idleColor;
    }

    private void Update()
    {
        if (canChangeScene && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            card.color = activeColor;
            canChangeScene = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            card.color = idleColor;
            canChangeScene = false;
        }
    }
}
