using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UShooting : MonoBehaviour
{
    [SerializeField] Rigidbody2D Bullet;

    [SerializeField] Transform[] firePoints;

    [SerializeField] float bulletSpeed;

    [SerializeField] float destroyBulletAfter;

    [SerializeField] float shootingCooldown;

    [SerializeField] bool isShooting;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isShooting)
        {
            StartCoroutine(BoombasticShooting());
        }
    }

    IEnumerator BoombasticShooting()
    {
        isShooting = true;
        foreach (var firePoint in firePoints)
        {
            var Instance = Instantiate(Bullet, firePoint.transform.position, Quaternion.identity);
            if (transform.localScale.x < 0)
            {
                Vector3 scaler = Instance.transform.localScale;
                scaler.x *= -1;
                Instance.transform.localScale = scaler;
            }
            Instance.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
            Destroy(Instance.gameObject, destroyBulletAfter);
        }
        yield return new WaitForSeconds(shootingCooldown);
        isShooting = false;
    }
}
