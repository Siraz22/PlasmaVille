using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TD_BulletScript : MonoBehaviour
{

    private Transform target;
    public float speed = 10f;

    public float damage=10f;

    public GameObject MuzzlePrefab;
    public GameObject HitPrefab;


    public void Seek(Transform _target)
    {
        target = _target;
    }

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
        if(target==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude<=distanceThisFrame)
        {
            Debug.Log("if called");
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame,Space.World);
    }

    void HitTarget()
    {
        Debug.Log("Hit Target");

        //Instantly destory target
        DNABott enemycurr = target.gameObject.GetComponent<DNABott>();

        if(enemycurr!=null)
        {
            enemycurr.TakeDamage(damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;


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
        
        Destroy(gameObject);
    }
}
