using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFirst : MonoBehaviour
{
    [SerializeField] GameObject CameraZoomOut;
    [SerializeField] GameObject CameraZoomIn;

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
        


        story.AddChild(initialSettings);
        story.AddChild(waitTimeBeforeZoom);

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

    private void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }
}
