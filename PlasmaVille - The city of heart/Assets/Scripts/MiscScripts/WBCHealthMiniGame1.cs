using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WBCHealthMiniGame1 : MonoBehaviour
{
    public static float HealthWBC = 100;

    public GameObject CanvasLost;

    // Update is called once per frame
    void Update()
    {
        
        if(HealthWBC<=0)
        {
            Cursor.lockState = CursorLockMode.None;
            CanvasLost.SetActive(true);
            //Time.timeScale = 0;
        }
    }
}
