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
    [SerializeField] GameObject ExitMenu;
    [SerializeField] TextMeshProUGUI timeCurrent;
    [SerializeField] TextMeshProUGUI timeBest;

    [Header("Save Settings")]
    [SerializeField] string SaveFileName;

    [Header("Scene Settings")]
    [SerializeField] string NextScene;
    [SerializeField] string PreviousScene;
    [SerializeField] string CurrentScene;
    [SerializeField] string MainMenuScene;


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

    //values
    float currentTime;

    //flags
    bool playerDead;
    bool halfTimeComplete;
    bool oneFourthTimeComplete;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        SavingValues();

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
        ExitMenu.SetActive(false);
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
            playerDead = true;
            timer = 0;
            timerStatus.text = ((int)timer).ToString();
            currentTime = timer;
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
            currentTime = timer;
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
        ExitMenuSettings();
        PlayerUI.SetActive(false);
        pauseManager.canPause = false;
        StopCameraShake();
        GameObject.FindGameObjectWithTag("Player").GetComponent<TTPlayerController>().OnDeath();
        anim.SetBool("isAlive", false);
        yield return new WaitForSeconds(waitTime);
        ExitMenu.SetActive(true);
    }

    void ExitMenuSettings()
    {
        timeCurrent.text = (timerEndsIn - currentTime).ToString("n2");
        if((timerEndsIn-currentTime) < PlayerPrefs.GetFloat(SaveFileName))
        {
            PlayerPrefs.SetFloat(SaveFileName, timerEndsIn - currentTime);
            timeBest.text = PlayerPrefs.GetFloat(SaveFileName).ToString("n2");
        }
        else
        {
            timeBest.text = PlayerPrefs.GetFloat(SaveFileName).ToString("n2");
        }
    }

    void SavingValues()
    {
        if(PlayerPrefs.GetFloat(SaveFileName) <= 0)
        {
            PlayerPrefs.SetFloat(SaveFileName, timerEndsIn);
        }
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

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(NextScene);
    }

    public void PreviousLevel()
    {
        SceneManager.LoadScene(PreviousScene);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}
