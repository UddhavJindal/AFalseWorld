using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTPortal : MonoBehaviour
{
    public bool portalOpen = false;
    bool inPortal = false;
    public int currentCoins = 0;
    public int maxCoins = 1;
    [SerializeField] int sceneNum; 
    private void Update()
    {
        if(inPortal && portalOpen && Input.GetKeyDown(KeyCode.E))
        {
            portalOpen = false;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeTrialMechanism>().isFinished = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inPortal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inPortal = false;
        }
    }
}
