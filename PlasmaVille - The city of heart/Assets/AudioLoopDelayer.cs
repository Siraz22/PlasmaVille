using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopDelayer : MonoBehaviour
{
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedLoop());
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator DelayedLoop()
    {
        for (int i = 0; i < 1000; i--)
        {
            sound.Stop();
            sound.Play();
            yield return new WaitForSeconds(Random.Range(10, 15));
        }
    }
}
