using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCameraTransition : MonoBehaviour
{
    [SerializeField] GameObject cameraZoomedOut;
    [SerializeField] GameObject cameraZoomedIn;

    //flag 
    [SerializeField] bool canStartTransition;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        rootNode = new RootNode();

        Sequence transitionBehaviour = new Sequence("Behaviour");

        Leaf initialSettings = new Leaf("Initial Settings", InitialSettings);
        Leaf changeTransition = new Leaf("Initial Settings", ChangingTransition);

        transitionBehaviour.AddChild(initialSettings);
        transitionBehaviour.AddChild(changeTransition);

        rootNode.AddChild(transitionBehaviour);
    }

    public Node.Status InitialSettings()
    {
        cameraZoomedOut.SetActive(true);
        cameraZoomedIn.SetActive(false);
        return Node.Status.SUCCESS;
    }

    public Node.Status ChangingTransition()
    {
        if(canStartTransition)
        {
            cameraZoomedOut.SetActive(false);
            cameraZoomedIn.SetActive(true);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canStartTransition = true;
        }
    }
}
