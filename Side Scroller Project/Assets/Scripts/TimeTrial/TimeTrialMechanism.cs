using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeTrialMechanism : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI timerStatus;
    [SerializeField] GameObject Destroyer;
    [SerializeField] GameObject PlayerUI;
    [SerializeField] GameObject LevelUI;
    [SerializeField] GameObject[] Cameras;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator anim;
    [SerializeField] PauseManager pauseManager;

    [Header("Audio")]
    [SerializeField] AudioClip soothingSFX;
    [SerializeField] AudioClip energeticSFX;
    
    [Header("Destroyer Points")]
    [SerializeField] Transform DestroyerInitialPoint;
    [SerializeField] Transform DestroyerFinalPoint;

    [Header("Settings")]
    [SerializeField] float timerEndsIn;
    [SerializeField] float destroyerSpeed = 5;
    [SerializeField] float displayLevelNameFor = 3;
    float timer;
    [HideInInspector] public bool isFinished;
    [HideInInspector] public bool isDead;

    //flags
    bool playerDead;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        rootNode = new RootNode();

        Sequence timeTrial = new Sequence("Time Trial Mechanism");

        Leaf mechanismSettings = new Leaf("Setting Timer Values", MechanismSettings);
        Leaf timer = new Leaf("Countdown Starts", Timer);
        Leaf destroyerStart = new Leaf("Destroyer Starts", DestroyerStart);
        Leaf playTransition = new Leaf("Playing Transition", PlayTransition);
        Leaf displayLevelName = new Leaf("Display Level Name", DisplayLevelName);
        Leaf endingMechanism = new Leaf("Ending Mechanism", EndingMechanism);

        timeTrial.AddChild(mechanismSettings);
        timeTrial.AddChild(playTransition);
        timeTrial.AddChild(displayLevelName);
        timeTrial.AddChild(timer);
        timeTrial.AddChild(destroyerStart);
        timeTrial.AddChild(endingMechanism);

        rootNode.AddChild(timeTrial);
    }

    public Node.Status MechanismSettings()
    {
        audioSource.clip = soothingSFX;
        audioSource.Play();
        timer = 0;
        timerStatus.text = ((int)timer).ToString();
        Destroyer.transform.position = DestroyerInitialPoint.transform.position;
        PlayerUI.SetActive(false);
        //LevelUI.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status PlayTransition()
    {
        anim.SetBool("isAlive", true);
        return Node.Status.SUCCESS;
    }

    public Node.Status DisplayLevelName()
    {
        timer += Time.deltaTime;
        if(timer >= displayLevelNameFor)
        {
            timer = timerEndsIn;
            LevelUI.SetActive(false);
            PlayerUI.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().canMove = true;
            pauseManager.canPause = true;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status Timer()
    {
        timer -= Time.deltaTime;
        timerStatus.text = ((int)timer).ToString();
        if (timer < 0)
        {
            timer = 0;
            timerStatus.text = ((int)timer).ToString();
            StartCameraShake();
            audioSource.clip = energeticSFX;
            audioSource.Play();
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status DestroyerStart()
    {
        if (Input.GetKeyDown(KeyCode.O) || isFinished)
        {
            isFinished = false;
            playerDead = false;
            
            return Node.Status.SUCCESS;
        }

        if(isDead)
        {
            isDead = false;
            playerDead = true;
            return Node.Status.SUCCESS;
        }

        if (Vector3.Distance(Destroyer.transform.position, DestroyerFinalPoint.transform.position) <= 0)
        {
            StopCameraShake();
            return Node.Status.SUCCESS;
        }
        else
        {
            Destroyer.transform.position = Vector3.MoveTowards(Destroyer.transform.position, DestroyerFinalPoint.transform.position, destroyerSpeed * Time.deltaTime);
            return Node.Status.RUNNING;
        }
    }

    public Node.Status EndingMechanism()
    {
        if(playerDead)
        {
            StartCoroutine(Changescene(1, 2));
        }
        else
        {
            StartCoroutine(Changescene(0, 2));
        }
        return Node.Status.SUCCESS;
    }

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }

    IEnumerator Changescene(int sceneNumber, float waitTime)
    {
        pauseManager.canPause = false;
        StopCameraShake();
        GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().canMove = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().OnDeath();
        anim.SetBool("isAlive", false);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneNumber);
    }

    void StartCameraShake()
    {
        foreach (GameObject camera in Cameras)
        {
            camera.GetComponent<TTCameraShake>().start = true;
        }
    }

    void StopCameraShake()
    {
        foreach (GameObject camera in Cameras)
        {
            camera.GetComponent<TTCameraShake>().start = false;
        }
    }
}
