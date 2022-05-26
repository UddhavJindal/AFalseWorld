using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTCollectable : MonoBehaviour
{
    [Header("Settings")]
    bool canPick;

    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().currentCoins++;
            if (GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().currentCoins == GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().maxCoins)
            {
                GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().portalOpen = true;
            }
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPick = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPick = false;
        }
    }
}
