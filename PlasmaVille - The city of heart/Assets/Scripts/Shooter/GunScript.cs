using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public bool isFiring;
    public MoveProjectile bullet;

    public Transform MuzzlePoint;

    private float TimeToFire = 0;
    public float firerate = 1;

    public AudioSource GunSound;
    // Start is called before the first frame update
    void Start()
    {
        GunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFiring)
        {
            if ((Time.time >= TimeToFire))
            {
                TimeToFire = Time.time + 1 / firerate;
                Shoot(); //This script just puts a raycast out of muzzle. Doesn't really shoot
            }
        }
    }

    void Shoot()
    {
        GunSound.Play();
        MoveProjectile curr_Bullet = Instantiate(bullet, MuzzlePoint.position, MuzzlePoint.rotation);

    }

}
