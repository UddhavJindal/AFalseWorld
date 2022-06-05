using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFirst : MonoBehaviour
{
    [SerializeField] GameObject CameraZoomOut;
    [SerializeField] GameObject CameraZoomIn;

    [SerializeField] AudioClip one;
    [SerializeField] AudioClip two;

    [SerializeField] AudioSource source;

    [SerializeField] GameObject Platform;

    //timer
    float timer;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        rootNode = new RootNode();

        Sequence story = new Sequence("Story");

        Leaf initialSettings = new Leaf("Initial Settings", InitialSettings);
        Leaf waitTimeBeforeZoom = new Leaf("Initial Settings", WaitTimeBeforeZoom);
        Leaf firstClip = new Leaf("Initial Settings", FirstClip);
        Leaf waitforFirst = new Leaf("Initial Settings", WaitingFirstForDialogueToFinish);
        Leaf secondOne = new Leaf("Initial Settings", SecondDialogueMechanism);



        story.AddChild(initialSettings);
        story.AddChild(waitTimeBeforeZoom);
        story.AddChild(firstClip);
        story.AddChild(waitforFirst);
        story.AddChild(secondOne);

        rootNode.AddChild(story);
    }

    public Node.Status InitialSettings()
    {
        CameraZoomOut.SetActive(true);
        CameraZoomIn.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status WaitTimeBeforeZoom()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status FirstClip()
    {
        source.clip = one;
        source.Play();
        return Node.Status.SUCCESS;
    }

    public Node.Status WaitingFirstForDialogueToFinish()
    {
        timer += Time.deltaTime;
        if(timer > 13)
        {
            timer = 0;
            source.clip = two;
            source.Play();
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status SecondDialogueMechanism()
    {
        timer += Time.deltaTime;
        if(timer > 17)
        {
            Platform.SetActive(false);
        }
        return Node.Status.RUNNING;
    }

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }
}
