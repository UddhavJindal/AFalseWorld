using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTCollectable : MonoBehaviour
{
    [SerializeField] bool canPick;

    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.LeftShift))
        {
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
