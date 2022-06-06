using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] TextMeshProUGUI GameTitle;
    [SerializeField] TextMeshProUGUI WarningTitle;
    [SerializeField] TextMeshProUGUI KeyStatus;
    [SerializeField] GameObject NameMenu;
    [SerializeField] TMP_InputField inputField;

    [Header("Settings")]
    [SerializeField] float fadeSpeed;
    [SerializeField] string SceneNumber;

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
        Leaf disclaimer = new Leaf("Fading Title", Disclaimer);
        Leaf waitToStart = new Leaf("Wait", WaitToChangeScene);
        Leaf nameMenu = new Leaf("Check Name", ShowingNameDialogue);

        titleManager.AddChild(starters);
        titleManager.AddChild(titleFade);
        titleManager.AddChild(disclaimer);
        titleManager.AddChild(waitToStart);
        titleManager.AddChild(nameMenu);

        rootNode.AddChild(titleManager);
    }

    public Node.Status Starters()
    {
        KeyStatus.gameObject.SetActive(false);
        WarningTitle.gameObject.SetActive(false);
        NameMenu.SetActive(false);
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
            currentAlphaValue = 0;
            GameTitle.gameObject.SetActive(false);
            WarningTitle.gameObject.SetActive(true);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status Disclaimer()
    {
        currentAlphaValue += Time.deltaTime * fadeSpeed;
        WarningTitle.alpha = currentAlphaValue;
        if (currentAlphaValue >= maxAlphaValue)
        {
            KeyStatus.gameObject.SetActive(true);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status WaitToChangeScene()
    {
        if(Input.anyKey)
        {
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status ShowingNameDialogue()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")) || string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
        {
            KeyStatus.gameObject.SetActive(false);
            WarningTitle.gameObject.SetActive(false);
            NameMenu.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneNumber);
        }
        return Node.Status.SUCCESS;
    }

    public void SavingName()
    {
        if(inputField.text != null)
        {
            PlayerPrefs.SetString("PlayerName" ,inputField.text);
            SceneManager.LoadScene(SceneNumber);
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
