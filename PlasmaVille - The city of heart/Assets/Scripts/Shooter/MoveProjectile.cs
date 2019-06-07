using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{

    public float MoveSpeed = 10f;

    public GameObject MuzzlePrefab;
    public GameObject HitPrefab;
    public float DamagePassed;

    // Use this for initialization
    void Start()
    {
        if (MuzzlePrefab != null)
        {
            var MuzzleVFX = Instantiate(MuzzlePrefab, transform.position, Quaternion.identity);
            MuzzleVFX.transform.forward = gameObject.transform.forward;

            //Debug.Log("Muzzle Flash");

            var PSMuzzle = MuzzleVFX.GetComponent<ParticleSystem>();
            if (PSMuzzle != null)
            {
                Destroy(MuzzleVFX, PSMuzzle.main.duration);
            }
            else
            {
                var psChild = MuzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(MuzzleVFX, psChild.main.duration);
            }
        }

        Destroy(gameObject, 10f);
        //Destory the gameobject regardless after 10 seconds
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveSpeed != 0)
        {
            transform.position += transform.forward * (MoveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;


        if (collision.transform.CompareTag("Enemy"))
        {
            //Enemy takes Damage
            collision.transform.GetComponent<EnemyScript>().TakeDamage(DamagePassed);
        }

        if (HitPrefab != null)
        {
            var hitVFX = Instantiate(HitPrefab, pos, rot);

            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitVFX, psHit.main.duration);

            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }

        }

        MoveSpeed = 0;
        Destroy(gameObject);
    }
}
