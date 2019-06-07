using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration,float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elasped = 0.0f;
        while (elasped < duration)
        {
            float x = transform.localPosition.x + Random.Range(-0.1f, 0.1f) * magnitude;
            float y = transform.localPosition.y + Random.Range(-0.1f, 0.1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elasped += Time.deltaTime;

            yield return null;
        }
        
        transform.localPosition = originalPos;
    }
}
