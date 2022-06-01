using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFMLeverMechanism : MonoBehaviour
{
    [SerializeField] bool leverSwitched = false;
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform[] shootPoints;

    void Update()
    {
        if(leverSwitched)
        {
            ShootProjectiles();
            //leverSwitched = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Q) && !leverSwitched)
            {
                leverSwitched = true;
            }
        }
    }

    void ShootProjectiles()
    {
        foreach (Transform point in shootPoints)
        {
            GameObject fired = Instantiate(bullet, point);
            Rigidbody rb = fired.GetComponent<Rigidbody>();
            rb.AddForce(Vector2.up * speed * Time.deltaTime);
        }
    }
}
