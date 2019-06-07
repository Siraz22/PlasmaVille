using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBCTurret : MonoBehaviour
{
    private Transform target;
    public float Range = 5f;
    public float rotateSpeed=5f;

    public float firerate = 1f;
    private float firecountdown = 0f;

    public GameObject BulletPrefab;
    public Transform Firepoint;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("TD_Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distancetoenemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(shortestDistance> distancetoenemy)
            {
                shortestDistance = distancetoenemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy!=null&&shortestDistance<=Range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion look_rotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, look_rotation, Time.deltaTime*rotateSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(firecountdown<=0f )
        {
            Shoot();
            firecountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        GameObject bulletGO= (GameObject)Instantiate(BulletPrefab, Firepoint.position, Firepoint.rotation);
        TD_BulletScript bullet = bulletGO.GetComponent<TD_BulletScript>();

        if(bullet!=null)
        {
            bullet.Seek(target);
        }
    }
}
