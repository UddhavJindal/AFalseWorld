using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTCollectable : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().currentCoins++;
            if (GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().currentCoins == GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().maxCoins)
            {
                GameObject.FindGameObjectWithTag("Portal").GetComponent<TTPortal>().portalOpen = true;
            }
            Destroy(gameObject);
        }
   }
}
