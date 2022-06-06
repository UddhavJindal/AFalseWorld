using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Secret : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
