using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color startColor;


    private Renderer rend;

    public Vector3 offset;

    public GameObject WBCTurret;

    Singletons buildmanager;

    private void OnMouseEnter()
    {
        if (!buildmanager.canBuild)
        {
            return;
        }

        if (buildmanager.hasExperience)
        {

            rend.material.color = hoverColor;

        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseDown()
    {
        if(!buildmanager.canBuild)
        {
            return;
        }

        if(WBCTurret!=null)
        {
            Debug.Log("Already a turret is present");
            return;
        }

        buildmanager.BuildTurretOn(this);

    }

    public Vector3 GetBuildPos()
    {
        return transform.position + offset;
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildmanager = Singletons.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
