using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject openPanel;
    public GameObject closePanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            openPanel.SetActive(true);
            closePanel.SetActive(false);
        }
    }
}
