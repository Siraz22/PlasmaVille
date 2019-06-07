using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Singletons : MonoBehaviour
{
    public AudioListener[] audioListeners;

    #region Singleton

    public static Singletons Instance;

    #endregion

    public Camera Isocam;
    public Camera TD_Cam;
    public NotesManager Notescript;

    public Button DialogueUI;   
    public TextMeshProUGUI DialogueTEXT;
    public DialogueManager dialoguescript;
    public GameObject DropDownAnimObj;
    public TextMeshProUGUI NotesPopTMProTEXT;

    public GameObject Notebook;
    public Image EmptyPage;

    public AudioManager AudioScript;

    public SequenceHandler SequenceScript;

    public Transform towerDefence_endpoint;

    public GameObject NeutrophilTurret;
    public GameObject KillerTurret;

    public GameObject BuildEffect;
    public GameObject LeaveWBCEffect;

    private WBCBlueprint turretTobuild;

    //property to get something
    public bool canBuild { get { return turretTobuild != null; } }
    public bool hasExperience { get { return TD_Stats.Experience >= turretTobuild.cost; } }

    private void Awake()
    {
        Instance = this;
        
        //Start the Game Theme
        AudioScript.GameThemeAS.clip = AudioScript.GameThemeAC;
        AudioScript.GameThemeAS.Play();
    }

    public void SelectTurretToBuild(WBCBlueprint turret)
    {
        turretTobuild = turret;
    }

    public void BuildTurretOn(NodeScript node)
    {
        if (TD_Stats.Experience < turretTobuild.cost)
        {
            Debug.Log("Not enough Experience");
            return;
        }

        TD_Stats.Experience -= turretTobuild.cost;

        GameObject turret = Instantiate(turretTobuild.prefab, node.GetBuildPos(), Quaternion.identity);
        node.WBCTurret = turret;

        GameObject Effect = Instantiate(BuildEffect, node.GetBuildPos(), Quaternion.identity);
        Destroy(Effect, 4f);

        Debug.Log("WBC Placed! " + "Experience Left" + TD_Stats.Experience);
    }

    public void Update()
    {
        audioListeners = FindObjectsOfType<AudioListener>();
    }
}
