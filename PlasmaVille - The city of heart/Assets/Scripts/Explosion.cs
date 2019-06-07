using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius = 5.0F;
    public float power = 10.0F;
    public CameraShake cameraShake;
    public float time = 0.0f;
    public AudioSource explosionSound;

    void Start()
    {
        StartCoroutine((cameraShake.Shake(0.2f, 10f)));
        explosionSound.Play();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2.0f)
        {
            StartCoroutine((cameraShake.Shake(0.2f, 10f)));
            explosionSound.Play();
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }

            time = 0f;
        }
    }
}
