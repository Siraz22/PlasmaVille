using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractableBench : MonoBehaviour
{

    public Camera IsoCam;
    
    private void Start()
    {     
        IsoCam = Singletons.Instance.Isocam;             
    }

    // Update is called once per frame
    void Update ()
    {
        transform.LookAt(transform.position+IsoCam.transform.rotation*Vector3.forward,IsoCam.transform.rotation*Vector3.up);
	}

}