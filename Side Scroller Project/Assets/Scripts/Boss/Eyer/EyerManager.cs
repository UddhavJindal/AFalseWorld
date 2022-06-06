using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyerManager : MonoBehaviour
{
    #region Varibales
    [Header("Camera Settings")]
    public GameObject zoomInCam;
    public GameObject zoomOutCam;

    [Header("Animator")]
    public Animator actionAnim;

    [Header("Script")]
    public EMovement movementScript;
    public EShooting shootingScript;

    [Header("Bullet Settings")]
    public int bulletAmts;
    public float bulletSpd;

    [Header("Health Settings")]
    public int health = 100;
    //int fullHealth = 100;

    [Header("Phase Settings")]
    public bool phaseOneActive;
    public bool phaseTwoActive;
    public bool zeroHealth;

    RootNode rootNode;
    [Header("Tree Status")]
    public Node.Status treeStatus = Node.Status.RUNNING;
    #endregion

    #region Pre-Defined Functions
    void Start()
    {
        rootNode = new RootNode();

        Sequence flyerMech = new Sequence("Eyer Flyer Mechanism");

        Leaf zoomIn = new Leaf("Zoom on Enemy", ZoomIn);
        Leaf zoomOut = new Leaf("Zoom off Enemy", ZoomOut);
        Leaf phaseOne = new Leaf("Enemy PhaseOne", PhaseOne);
        Leaf phaseTwo = new Leaf("Enemy PhaseTwo", PhaseTwo);
        Leaf enemyDead = new Leaf("Enemy Dead", EnemyDead);

        flyerMech.AddChild(zoomIn);
        flyerMech.AddChild(zoomOut);
        flyerMech.AddChild(phaseOne);
        flyerMech.AddChild(phaseTwo);
        //flyerMech.AddChild(enemyDead);

        rootNode.AddChild(flyerMech);
    }

    void Update()
    {
        GetComponent<EShooting>().amount = bulletAmts;
        GetComponent<EShooting>().bulletSpeed = bulletSpd;

        if(50 < health && health <= 100 )
        {
            phaseOneActive = true;
            phaseTwoActive = false;
            zeroHealth = false;
        }
        if(0 < health && health <= 50 )
        {
            phaseOneActive = false;
            phaseTwoActive = true;
            zeroHealth = false;
        }
        if (health == 0)
        {
            phaseOneActive = false;
            phaseTwoActive = false;
            zeroHealth = true;
        }

        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = rootNode.Process();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            health--;
        }
    }

    #endregion

    #region Node Function
    public Node.Status ZoomIn()
    {
        zoomInCam.SetActive(true);
        zoomOutCam.SetActive(false);
        movementScript.enabled = false;
        shootingScript.enabled = false;
        actionAnim.enabled = false;
        return Node.Status.SUCCESS;
    }

    public Node.Status ZoomOut()
    {
        zoomInCam.SetActive(false);
        zoomOutCam.SetActive(true);
        movementScript.enabled = false;
        shootingScript.enabled = false;
        actionAnim.enabled = false;
        return Node.Status.SUCCESS;
    }

    public Node.Status PhaseOne()
    {
        if(phaseOneActive)
        {
            movementScript.enabled = true;
            shootingScript.enabled = true;
            actionAnim.enabled = true;
            bulletAmts = 4;
            bulletSpd = 750;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status PhaseTwo()
    {
        if(phaseTwoActive)
        {
            //movementScript.enabled = false;
            bulletAmts = 8;
            bulletSpd = 1000;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status EnemyDead()
    {
        if(zeroHealth)
        {
            movementScript.enabled = false;
            shootingScript.enabled = false;
            gameObject.SetActive(false);
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }
    #endregion
}
