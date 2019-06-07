using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyWBCTurret : MonoBehaviour
{
    public Image TimerToLeave;
    private float counter = 10f;
    public GameObject EndEffect;

    bool EndEffectPlayed = false;

    public float SmoothLerpSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        EndEffect = Singletons.Instance.LeaveWBCEffect;
        TimerToLeave = gameObject.GetComponentInChildren<Image>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter<=1f&&!EndEffectPlayed)
        {
            Instantiate(EndEffect, transform.position, Quaternion.identity);
            EndEffectPlayed = true;
        }

        counter -= Time.deltaTime;

        TimerToLeave.fillAmount = Mathf.Lerp(TimerToLeave.fillAmount, counter/10f, Time.deltaTime * SmoothLerpSpeed);
    }
}
