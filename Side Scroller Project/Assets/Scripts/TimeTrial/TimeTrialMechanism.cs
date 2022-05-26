using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialMechanism : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI timerStatus;
    [SerializeField] GameObject Destroyer;
    [SerializeField] GameObject PlayerUI;
    [SerializeField] GameObject LevelUI;

    [Header("Destroyer Points")]
    [SerializeField] Transform DestroyerInitialPoint;
    [SerializeField] Transform DestroyerFinalPoint;

    [Header("Transition Components")]
    [SerializeField] Transform UpperTGFX;
    [SerializeField] Transform LowerTGFX;
    [SerializeField] Transform UInitionPoint;
    [SerializeField] Transform LInitialPoint;
    [SerializeField] Transform UFinalPoint;
    [SerializeField] Transform LFinalPoint;

    [Header("Settings")]
    [SerializeField] float timerEndsIn;
    [SerializeField] float transitionSpeed = 5;
    [SerializeField] float destroyerSpeed = 5;
    [SerializeField] float displayLevelNameFor = 3;
    float timer;

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

        timeTrial.AddChild(mechanismSettings);
        timeTrial.AddChild(playTransition);
        timeTrial.AddChild(displayLevelName);
        timeTrial.AddChild(timer);
        timeTrial.AddChild(destroyerStart);

        rootNode.AddChild(timeTrial);
    }

    public Node.Status MechanismSettings()
    {
        timer = 0;
        timerStatus.text = ((int)timer).ToString();
        Destroyer.transform.position = DestroyerInitialPoint.transform.position;
        UpperTGFX.position = UInitionPoint.position;
        LowerTGFX.position = LInitialPoint.position;
        PlayerUI.SetActive(false);
        //LevelUI.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status PlayTransition()
    {
        UpperTGFX.position = Vector3.MoveTowards(UpperTGFX.position, UFinalPoint.position, transitionSpeed * Time.deltaTime);
        LowerTGFX.position = Vector3.MoveTowards(LowerTGFX.position, LFinalPoint.position, transitionSpeed * Time.deltaTime);
        if(Vector3.Distance(UpperTGFX.position, UFinalPoint.position) <= 0 && Vector3.Distance(LowerTGFX.position, LFinalPoint.position) <= 0)
        {
            UpperTGFX.gameObject.SetActive(false);
            LowerTGFX.gameObject.SetActive(false);
            LevelUI.SetActive(true);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
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
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status DestroyerStart()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            return Node.Status.SUCCESS;
        }

        if (Vector3.Distance(Destroyer.transform.position, DestroyerFinalPoint.transform.position) <= 0)
        {
            return Node.Status.SUCCESS;
        }
        else
        {
            Destroyer.transform.position = Vector3.MoveTowards(Destroyer.transform.position, DestroyerFinalPoint.transform.position, destroyerSpeed * Time.deltaTime);
            return Node.Status.RUNNING;
        }
    }

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }
}
