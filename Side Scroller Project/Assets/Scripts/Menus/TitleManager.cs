using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] TextMeshProUGUI GameTitle;

    [Header("Settings")]
    [SerializeField] float fadeSpeed;
    [SerializeField] int SceneNumber;

    float currentAlphaValue;
    float maxAlphaValue;

    RootNode rootNode;

    public Node.Status treeStatus = Node.Status.RUNNING;

    private void Start()
    {
        rootNode = new RootNode();

        Sequence titleManager = new Sequence("TitleManager Mech");

        Leaf starters = new Leaf("Starters", Starters);
        Leaf titleFade = new Leaf("Fading Title", TitleFade);

        titleManager.AddChild(starters);
        titleManager.AddChild(titleFade);

        rootNode.AddChild(titleManager);
    }

    public Node.Status Starters()
    {
        GameTitle.alpha = 0;
        currentAlphaValue = 0;
        maxAlphaValue = 1;
        return Node.Status.SUCCESS;
    }

    public Node.Status TitleFade()
    {
        currentAlphaValue += Time.deltaTime * fadeSpeed;
        GameTitle.alpha = currentAlphaValue;
        if(currentAlphaValue >= maxAlphaValue)
        {
            SceneManager.LoadScene(SceneNumber);
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
