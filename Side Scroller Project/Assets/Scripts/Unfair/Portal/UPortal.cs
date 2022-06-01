using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UPortal : MonoBehaviour
{
    [SerializeField] string sceneName;

    [SerializeField] bool canEnter;

    [SerializeField] bool isOpen;

    private void Update()
    {
        if (canEnter && isOpen && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canEnter = true;
        }
        else
        {
            canEnter = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canEnter = false;
        }
    }
}
