using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UDoor : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ParticleSystem endBoom;
    [SerializeField] GameObject Player;

    [SerializeField] AudioClip[] souldClips;

    [Header("Settings")]
    [SerializeField] string sceneName;
    [SerializeField] float changeSceneIn;
    [SerializeField] float noOfBooms;

    

    //flag
    bool canOpenDoor;

    bool doorClosed;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpenDoor && !doorClosed)
        {
            doorClosed = false;
            Destroy(Player.gameObject);
            StartCoroutine(Instancy());
            StartCoroutine(ChangeScene());

            GameObject.FindGameObjectWithTag("DMSource").GetComponent<AudioSource>().clip = souldClips[Random.Range(0, souldClips.Length)];
            GameObject.FindGameObjectWithTag("DMSource").GetComponent<AudioSource>().Play();
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

    IEnumerator Instancy()
    {
        for (int i = 0; i < noOfBooms; i++)
        {
            InstantiatingParticleSystem();
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(changeSceneIn);
        SceneManager.LoadScene(sceneName);
    }

    void InstantiatingParticleSystem()
    {
        Vector3 rand = transform.position + Random.insideUnitSphere;
        var Instance = Instantiate(endBoom, new Vector3(rand.x, rand.y, transform.position.z), Quaternion.identity);
        Destroy(Instance.gameObject, 1);
    }
}
