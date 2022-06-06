using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShooting : MonoBehaviour
{
    #region Private Variable

    float timer;
    float timeBase;

    Vector2 bulDir;

    #endregion

    #region Serialize and Public Variables

    [Header("Component")]
    [SerializeField] Transform firePoint;

    [Header("Time Settings")]
    public float mechMinTime;
    public float mechMaxTime;

    [Header("Projectile Settings")]
    public int amount;
    [SerializeField] float startAngle, endAngle;

    [Header("Bullet Settings")]
    public float bulletSpeed = 10;
    [SerializeField] float bulletDeactivateTime = 5;

    #endregion

    #region In-Built Functions

    void Start()
    {
        timeBase = Random.Range(mechMinTime, mechMaxTime);
    }

    void Update()
    {
        ProjectileRepeater();
    }

    #endregion

    #region Custom Functions

    void ProjectileRepeater()
    {
        timer += Time.deltaTime;
        if (timer > timeBase)
        {
            InstancingCircleProjectile();
            timer = 0;
            timeBase = Random.Range(mechMinTime, mechMaxTime);
        }
    }

    void InstancingCircleProjectile()
    {
        float angleStep = ((endAngle - startAngle) / amount);
        float angle = startAngle;

        for (int i = 0; i < amount; i++)
        {
            float bulDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 100);
            float bulDirY = firePoint.position.y + Mathf.Cos((angle * Mathf.PI) / 100);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0);
            bulDir = (bulMoveVector - firePoint.position).normalized;

            StartCoroutine(Fire());

            angle += angleStep;
        }
        CMShakeScript.Instance.CameraShake(2f, 0.1f);
    }

    IEnumerator Fire()
    {
        GameObject bullet = EnemyPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulDir * bulletSpeed);
            yield return new WaitForSeconds(bulletDeactivateTime);
            bullet.SetActive(false);
        }
    }

    #endregion
}
