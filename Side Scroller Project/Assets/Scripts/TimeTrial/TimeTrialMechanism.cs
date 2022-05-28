using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeTrialMechanism : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI timerStatus;
    [SerializeField] GameObject PlayerUI;
    [SerializeField] GameObject LevelUI;
    [SerializeField] GameObject[] Cameras;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator anim;
    [SerializeField] PauseManager pauseManager;

    [Header("Audio")]
    [SerializeField] AudioClip soothingSFX;

    [Header("Settings")]
    [SerializeField] float timerEndsIn;
    [SerializeField] float displayLevelNameFor = 3;
    float timer;
    [HideInInspector] public bool isFinished;
    [HideInInspector] public bool isDead;

    //timer calculations
    float halfTime;
    float oneFourthTime;

    //flags
    bool playerDead;
    bool halfTimeComplete;
    bool oneFourthTimeComplete;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        rootNode = new RootNode();

        Sequence timeTrial = new Sequence("Time Trial Mechanism");

        Leaf mechanismSettings = new Leaf("Setting Timer Values", MechanismSettings);
        Leaf timer = new Leaf("Countdown Starts", Timer);
        Leaf playTransition = new Leaf("Playing Transition", PlayTransition);
        Leaf displayLevelName = new Leaf("Display Level Name", DisplayLevelName);
        Leaf endingMechanism = new Leaf("Ending Mechanism", EndingMechanism);

        timeTrial.AddChild(mechanismSettings);
        timeTrial.AddChild(playTransition);
        timeTrial.AddChild(displayLevelName);
        timeTrial.AddChild(timer);
        timeTrial.AddChild(endingMechanism);

        rootNode.AddChild(timeTrial);
    }

    public Node.Status MechanismSettings()
    {
        audioSource.clip = soothingSFX;
        audioSource.Play();
        halfTime = timerEndsIn / 2;
        oneFourthTime = timerEndsIn / 4;
        timer = 0;
        timerStatus.text = ((int)timer).ToString();
        PlayerUI.SetActive(false);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().OnDeath();
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
            playerDead = true;
            return Node.Status.SUCCESS;
        }

        if(!halfTimeComplete && timer < halfTime)
        {
            StartCameraShake(0.03f);
            halfTimeComplete = true;
        }

        if(!oneFourthTimeComplete && timer < oneFourthTime)
        {
            StartCameraShake(0.05f);
            oneFourthTimeComplete = true;
        }

        if (Input.GetKeyDown(KeyCode.O) || isFinished)
        {
            isFinished = false;
            playerDead = false;
            return Node.Status.SUCCESS;
        }

        return Node.Status.RUNNING;
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
        PlayerUI.SetActive(false);
        pauseManager.canPause = false;
        StopCameraShake();
        GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().OnDeath();
        anim.SetBool("isAlive", false);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneNumber);
    }

    void StartCameraShake(float strength)
    {
        foreach (GameObject camera in Cameras)
        {
            camera.GetComponent<TTCameraShake>().start = true;
            camera.GetComponent<TTCameraShake>().strength = strength;
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
