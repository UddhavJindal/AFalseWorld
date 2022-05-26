using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TTPlayerCollectable : MonoBehaviour
{
    #region Variables
    private PlayerInputs plyInput;
    private InputAction plyAction;

    [Header("Collectable Settings")]
    public int totalCount;
    int collectCount;
    bool coinCheck = false;
    bool portalOpen = false;
    bool canExit = false;

    [Header("References")]
    public Renderer portalSprite;
    public Color portalColor;
    #endregion

    #region Pre-Defined Functions
    void Awake()
    {
        plyInput = new PlayerInputs();
    }

    void Update()
    {
        if (collectCount == totalCount)
        {
            portalOpen = true;
            portalSprite.material.color = portalColor;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collects")
        {
            if (coinCheck == true)
            {
                Destroy(collision.gameObject);
                collectCount++;
            }
        }
        if (collision.gameObject.tag == "Portal")
        {
            if (portalOpen)
            {
                canExit = true;
            }
        }
    }

    void OnEnable()
    {
        plyInput.Player.Collecting.started += CollectAction;
        plyInput.Player.Collecting.Enable();

        plyInput.Player.Portal.started += ExitLevel;
        plyInput.Player.Portal.Enable();
    }

    void OnDisable()
    {
        plyInput.Player.Collecting.Disable();
        plyInput.Player.Portal.Disable();
    }
    #endregion

    #region Custom Functions
    void CollectAction(InputAction.CallbackContext obj)
    {
        coinCheck = true;
    }
    void ExitLevel(InputAction.CallbackContext obj)
    {
        if (canExit == true)
        {
            Debug.Log("Level Finished");
            //SceneManager.LoadScene("");
        }
    }
    #endregion
}
