using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTDestroyer : MonoBehaviour
{
    bool isDead = false;
    public int sceneNum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isDead)
        {
            isDead = true;
            StartCoroutine(NextScene());
            GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().canMove = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().OnDeath();
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneNum);
    }
}
