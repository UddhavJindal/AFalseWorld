using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GThreeMuskets : MonoBehaviour
{
    [SerializeField] Transform ThreeMuskets;

    [SerializeField] Vector3 axis;

    [SerializeField] float speed;

    private void Update()
    {
        ThreeMuskets.transform.Rotate(axis * speed * Time.deltaTime);
    }
}
