using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UInvisibleBoxOne : MonoBehaviour
{
    [SerializeField] GameObject Object;

    [SerializeField] float waitTimeBeforeDisabling;

    //flags
    bool disableCollision;

    private void Start()
    {
        Object.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !disableCollision)
        {
            disableCollision = true;
            StartCoroutine(DisablingBlock());
        }
    }

    IEnumerator DisablingBlock()
    {
        Object.SetActive(true);
        yield return new WaitForSeconds(waitTimeBeforeDisabling);
        Object.SetActive(false);
    }
}
