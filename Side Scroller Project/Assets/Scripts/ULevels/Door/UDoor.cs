using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UDoor : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ParticleSystem endBoom;
    [SerializeField] GameObject Player;

    [Header("Settings")]
    [SerializeField] string sceneName;
    [SerializeField] float changeSceneIn;

    //flag
    bool canOpenDoor;

    bool doorClosed;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpenDoor && !doorClosed)
        {
            doorClosed = false;
            Destroy(Player.gameObject);
            InstantiatingParticleSystem();
            StartCoroutine(ChangeScene());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canOpenDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canOpenDoor = false;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(changeSceneIn);
        SceneManager.LoadScene(sceneName);
    }

    void InstantiatingParticleSystem()
    {
        var Instance = Instantiate(endBoom, Player.transform.position, Quaternion.identity);
        Destroy(Instance.gameObject, 1);
    }
}
