using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            StartCoroutine(NextScene());
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

    IEnumerator NextScene()
    {
        portalOpen = false;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeTrialMechanism>().isFinished = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneNum);
    }
}
