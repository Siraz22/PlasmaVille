using UnityEngine;

public class PivotFollowPlayer : MonoBehaviour
{

    public Transform PlayerTarget;
    public float SmoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector3 desired_pos = PlayerTarget.position;
        Vector3 Smoothpos = Vector3.Lerp(transform.position, desired_pos, SmoothSpeed * Time.deltaTime);
        transform.position = Smoothpos;
    }
    
}
