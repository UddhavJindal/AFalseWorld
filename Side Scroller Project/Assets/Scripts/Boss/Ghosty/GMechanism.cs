using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMechanism : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject CameraZoomedOut;
    [SerializeField] GameObject CameraZoomedIn;
    [SerializeField] GameObject ThreeMuskets;
    [SerializeField] Collider2D bossCollider;
    [SerializeField] GameObject Player;

    [Header("Refrences")]
    [SerializeField] GMovement gMovementScript;
    [SerializeField] GThreeMuskets gThreeMusketsScript;
    [SerializeField] Animator ghostAnim;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource bossSource;

    [Header("Sounds")]
    [SerializeField] AudioClip BossRoar;
    [SerializeField] AudioClip BossMusicOne;

    [Header("Bools")]
    public bool canStart;
    [SerializeField] bool canStartMechanismTwo;

    [Header("Settings")]
    [SerializeField] float RoarTime = 1.5f;
    [SerializeField] float RoarIntensity = 1;

    float timer;

    RootNode rootNode;

    [Header("Debug")]
    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        rootNode = new RootNode();

        Sequence behaviour = new Sequence("Behaviour");

        Leaf initialSettings = new Leaf("Initial Settings", InitialSettings);
        Leaf waitingToStartFight = new Leaf("Initial Settings", WaitingToStartFight);
        Leaf cameraTransitionIn = new Leaf("Camera Transition", CameraTransitionIn);
        Leaf ghostRoar = new Leaf("Initial Settings", GhostRoar);
        Leaf cameraTransitionOut = new Leaf("Initial Settings", CameraTransitionOut);
        Leaf startingMovement = new Leaf("Initial Settings", StartingMovement);
        Leaf canStartMechanismTwo = new Leaf("Initial Settings", CanStartMechanismTwo);
        Leaf startingThreeMuskets = new Leaf("Initial Settings", StartingMuskets);

        behaviour.AddChild(initialSettings);
        behaviour.AddChild(waitingToStartFight);
        behaviour.AddChild(cameraTransitionIn);
        behaviour.AddChild(ghostRoar);
        behaviour.AddChild(cameraTransitionOut);
        behaviour.AddChild(startingMovement);
        behaviour.AddChild(canStartMechanismTwo);
        behaviour.AddChild(cameraTransitionIn);
        behaviour.AddChild(ghostRoar);
        behaviour.AddChild(cameraTransitionOut);
        behaviour.AddChild(startingThreeMuskets);

        rootNode.AddChild(behaviour);
    }

    public Node.Status InitialSettings()
    {
        bossCollider.enabled = false;
        gMovementScript.enabled = false;
        gThreeMusketsScript.enabled = false;
        CameraZoomedIn.SetActive(false);
        ThreeMuskets.SetActive(false);
        return Node.Status.SUCCESS;
    }
    public Node.Status WaitingToStartFight()
    {
        if(canStart)
        {
            CameraZoomedIn.SetActive(true);
            CameraZoomedOut.SetActive(false);
            Player.GetComponent<TTPlayerController>().canMove = false;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status CameraTransitionIn()
    {
        timer += Time.deltaTime;
        if(timer > 3)
        {
            ghostAnim.SetBool("IsNotSleepy", true);
            timer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status GhostRoar()
    {
        bossSource.clip = BossRoar;
        bossSource.Play();
        CMShakeScript.Instance.CameraShake(RoarIntensity, RoarTime);
        return Node.Status.SUCCESS;
    }

    public Node.Status CameraTransitionOut()
    {
        timer += Time.deltaTime;
        if(timer > 3)
        {
            source.clip = BossMusicOne;
            source.Play();
            timer = 0;
            ghostAnim.SetBool("IsIdling", true);
            CameraZoomedIn.SetActive(false);
            CameraZoomedOut.SetActive(true);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status StartingMovement()
    {
        Player.GetComponent<TTPlayerController>().canMove = true;
        bossCollider.enabled = true;
        gMovementScript.enabled = true;
        return Node.Status.SUCCESS;
    }

    public Node.Status CanStartMechanismTwo()
    {
        if(canStartMechanismTwo)
        {
            Player.GetComponent<TTPlayerController>().canMove = false;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ghostAnim.SetBool("IsIdling", false);
            CameraZoomedIn.SetActive(true);
            CameraZoomedOut.SetActive(false);
            gMovementScript.enabled = false;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status StartingMuskets()
    {
        Player.GetComponent<TTPlayerController>().canMove = true;
        gThreeMusketsScript.enabled = true;
        gMovementScript.enabled = true;
        ThreeMuskets.SetActive(true);
        return Node.Status.SUCCESS;
    }

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }
}
