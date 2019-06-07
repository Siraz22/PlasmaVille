using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SequenceHandler : MonoBehaviour
{
    [Header("Starting Cinematic Elements")]
    public Image InstructionUI;
    public Button SetPlayerButton;
    public string[] PlayerStartDialogues;

    [Header("Mini Game 1 Elements")]
    public GameObject Wbc;
    public GameObject enemySpwaner;
    private bool loadWBC = false;
    public GameObject WbcCam;
    public GameObject[] buildings;
    private int EnemiesCount = 6;
    public GameObject[] Npcdeactivate;
    public Transform WBCActual;
    public Transform[] zombieTransform;
    //public string[] WBCEndDialogues;
    public GameObject AllWeakWBC;
    public GameObject Cutscene2Props;
    public GameObject VirtualCameras;
    public GameObject[] destroyAfterMiniGame1;
    public GameObject activeAfterMinigame1;

    [Header("Mini Game 2 Elements")]
    public GameObject Cinematic3_props;
    public string[] MiniGame2Dialogue;
    public GameObject CinematicCanvas;
    public GameObject Checkpoints;
    public GameObject Barricades;
    public AudioSource CharacterAudioSource;
    public GameObject[] FirstThreeBarriers;
    public GameObject Finisher;
    public GameObject WorldRoadCirculation;
    public GameObject MiniGameCirculationRoads;
    public GameObject fakePlayer;
    public GameObject MainMap;
    public bool Game2Ended = false;

    [Header("Tower Defence Game")]
    public GameObject[] RoadsWorld;
    public GameObject[] RoadsTD;
    public GameObject[] Pallets;
    public GameObject TDHolder;
    public Camera TD_Cam;
    bool TDEnded = false;
    public GameObject[] ConflictBench;
    public QuestionGenerator quesScript;

    [Header("Ending")]
    public GameObject cinematic4Props;
    public GameObject Boundary;
    bool gameFinished = false;

    [Header("Common Elements")]
    public GameObject playerCam;
    public GameObject playerRef;
    public GameObject DialogueBox;
    public GameObject MasterCam;
    public DialogueManager dialoguescript;
    //public GameObject Minimap;
    public GameObject Tutorial;
    public GameObject notesCollect;
    public GameObject SahilCam;
    public Button playButton;
    public AudioSource trainAudio;
    public GameObject ImageStarting;

    private bool loadCirculation = false;
    private int NoteCounter = 0;
    private bool Played;
    private bool play;
    private bool minimGame2Playing = false;
    public bool minigame2finished = false;

    [SerializeField]
    private int CutSceneNum = 0;

    public PlayableDirector[] Cutscenes;
    public BoxCollider[] MissionTriggerColliders; //0 - Gun Mission , 1 - Drill

    private void Awake()
    {
        //dialoguescript = Singletons.Instance.dialoguescript;

        Cutscenes = GetComponentsInChildren<PlayableDirector>();
        MasterCam = transform.GetChild(0).gameObject;


    }

    #region StartSettings

    public void StartSet()
    {
        playerRef.GetComponent<NavMeshPlayerMvt>().enabled = true;
        InstructionUI.gameObject.SetActive(false);
        Destroy(InstructionUI); //Don't need it anymore

        DialogueManager.PhotoID = 0;
        dialoguescript.NextNoteDialogueText(PlayerStartDialogues);

        
    }

    #endregion

    #region Mini Game 1

    void MiniGame1()
    {
        if (loadWBC)
        {
            if (Cutscenes[1].state != PlayState.Playing)
            {
                //Cutscene se pehle cahiye
                //playerRef.SetActive(false);
                Singletons.Instance.AudioScript.miniGame2Cutscene.Stop();
                Singletons.Instance.AudioScript.miniGame2Gameplay.Play();
                WbcCam.SetActive(true);
                MasterCam.SetActive(false);
                VirtualCameras.SetActive(false);

                for (int i = 0; i < zombieTransform.Length; ++i)
                {
                    zombieTransform[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < Npcdeactivate.Length; i++)
                {
                    Npcdeactivate[i].SetActive(false);
                }
                Wbc.gameObject.SetActive(false);
                WBCActual.position = Wbc.transform.position;
                WBCActual.rotation = Wbc.transform.rotation;
                WBCActual.gameObject.SetActive(true);
                enemySpwaner.SetActive(true);

                //Disable WBC weak
                AllWeakWBC.gameObject.SetActive(false);

                //Minimap.SetActive(true);

                loadWBC = false;
            }
        }
    }

    public IEnumerator FinishMiniGame1(bool passed)
    {
        //Resets everything back to normal
        Debug.Log("Finished Mini game 1");

        ImageStarting.SetActive(true);
        ImageStarting.GetComponent<Animation>().Play("FadeInFadeout");
        yield return new WaitForSeconds(3f);
        //Some Camera Effect

        //Back to normal
        playerCam.SetActive(true);
        playerRef.SetActive(true);
        WBCActual.gameObject.SetActive(false);
        WbcCam.SetActive(false);
        Singletons.Instance.AudioScript.miniGame2Gameplay.Stop();
        Singletons.Instance.AudioScript.GameThemeAS.Play();

        if (passed)
        {
            MissionTriggerColliders[0].gameObject.SetActive(false);
            MissionTriggerColliders[1].gameObject.SetActive(true);
            for (int i = 0; i < destroyAfterMiniGame1.Length; i++)
            {
                Destroy(destroyAfterMiniGame1[i]);
            }
            activeAfterMinigame1.SetActive(true);
        }
        else
        {
            MissionTriggerColliders[0].gameObject.SetActive(true);
            AllWeakWBC.SetActive(true);
            Cutscene2Props.SetActive(false);
        }

        yield return new WaitForSeconds(0f);
        ImageStarting.SetActive(false);

    }

    #endregion

    #region Mini Game 2

    void MiniGame2()
    {
        if (loadCirculation)
        {
            if (Cutscenes[2].state != PlayState.Playing)
            {
                //Do stuff for Mini Game 2 when cinematics end

                MasterCam.SetActive(false);
                CinematicCanvas.gameObject.SetActive(false);
                playerCam.SetActive(true);
                playerRef.transform.position = fakePlayer.transform.position;
                playerRef.transform.rotation = fakePlayer.transform.rotation;
                fakePlayer.SetActive(false);
                playerRef.SetActive(true);
                playerRef.GetComponent<NavMeshPlayerMvt>().enabled = true;
                playerRef.GetComponent<NavMeshPlayerMvt>().playerAgent.Stop();
                playerRef.GetComponent<NavMeshPlayerMvt>().playerAgent.ResetPath();
                minimGame2Playing = true;
                playerRef.GetComponent<NavMeshPlayerMvt>().speedPercent = 0f; //To stop the sound
                CharacterAudioSource.volume = 1f;
                playerRef.GetComponent<Rigidbody>().isKinematic = false;
                //Prompt player Dialogue

                DialogueManager.PhotoID = 0;
                dialoguescript.NextNoteDialogueText(MiniGame2Dialogue);

                

                Barricades.SetActive(true);
                Checkpoints.SetActive(true);

                foreach (GameObject currBarr in FirstThreeBarriers)
                {
                    currBarr.SetActive(true);
                }

                Finisher.SetActive(true);
                //Minimap.SetActive(true);
                MiniGameCirculationRoads.SetActive(true);
                WorldRoadCirculation.SetActive(false);

                loadCirculation = false;


            }
        }
    }

    public IEnumerator FinishMiniGame2(bool passed)
    {
        //Resets everything back to normal
        Debug.Log("Finished Mini game 2");

        ImageStarting.SetActive(true);
        ImageStarting.GetComponent<Animation>().Play("FadeInFadeout");
        minimGame2Playing = false;
        yield return new WaitForSeconds(3f);
        //Some Camera Effect

        //Back to normal
        playerCam.SetActive(true);
        playerRef.SetActive(true);
        playerRef.GetComponent<Rigidbody>().isKinematic = true;
        Cinematic3_props.SetActive(false); //OP statement
        Barricades.SetActive(false);
        Checkpoints.SetActive(false);
        WorldRoadCirculation.SetActive(true);
        MiniGameCirculationRoads.SetActive(false);

        if (passed)
            MissionTriggerColliders[2].gameObject.SetActive(true);
        else
            MissionTriggerColliders[1].gameObject.SetActive(true);

        minigame2finished = true;

        yield return new WaitForSeconds(0.5f);
        ImageStarting.SetActive(false);
    }

    #endregion

    #region Tower Defence

    public void CheckTD()
    {
        if (TD_Stats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        TDEnded = true;
        Debug.Log("Game Ended");
    }

    public IEnumerator FinishMiniGame3(bool passed)
    {
        //Resets everything back to normal
        Debug.Log("Finished Mini game 3");

        ImageStarting.SetActive(true);
        ImageStarting.GetComponent<Animation>().Play("FadeInFadeout");
        yield return new WaitForSeconds(3f);
        //Some Camera Effect

        //Back to normal
        playerCam.SetActive(true);
        playerRef.SetActive(true);

        TD_Cam.gameObject.SetActive(false);
        TDHolder.SetActive(false);

        foreach (GameObject bench in ConflictBench)
        {
            bench.SetActive(true);
        }
        
        if (!passed)
            MissionTriggerColliders[2].gameObject.SetActive(true);
        else
            MissionTriggerColliders[3].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        ImageStarting.SetActive(false);
    }

    #endregion

    #region Ending
    public void Ending()
    {
        if (!gameFinished)
        {
            if (Cutscenes[3].state != PlayState.Playing)
            {
                gameFinished = true;
            }
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        DialogueBox = Singletons.Instance.DialogueUI.gameObject;
    }

    public IEnumerator PlayButtonPressed()
    {

        yield return new WaitForSeconds(2.45f);
        Cutscenes[0].Play();
        trainAudio.Play();
        playerRef.GetComponent<AudioListener>().enabled = true;
        Played = true;
        play = false;
        SahilCam.SetActive(false);
        MasterCam.SetActive(true);
    }

    private void Update()
    {

        if (play)
        {
            StartCoroutine(PlayButtonPressed());
        }
        if (Played)
            CutsceneUpdate(0);

        MiniGame1();
        MiniGame2();

        if (!TDEnded)
        {
            CheckTD();
        }

        if (TDEnded && Singletons.Instance.Notescript.noofNotesCollected == 19)
        {
            MissionTriggerColliders[3].gameObject.SetActive(true);
        }

        if (minimGame2Playing)
        {
            if (Input.GetKeyDown(KeyCode.M))
                MainMap.SetActive(true);
            else if (Input.GetKeyUp(KeyCode.M))
                MainMap.SetActive(false);
        }
        else
        {
            MainMap.SetActive(false);
        }

        if(minigame2finished&&Input.GetMouseButton(1))
        {
            ImageStarting.gameObject.SetActive(false);
            minigame2finished = false;
        }
    }

    void CutsceneUpdate(int index)
    {
        if (Cutscenes[index].state != PlayState.Playing && Played)
        {
            playerCam.gameObject.SetActive(true);

            Played = false;


            //Called when the first cinematic goes off too
            if (InstructionUI != null)
            {
                //Minimap.SetActive(true);
                Tutorial.SetActive(true);
                notesCollect.SetActive(true);
                InstructionUI.gameObject.SetActive(true);
            }

            MasterCam.SetActive(false);

        }
    }

    public void CheckNoOfNotes()
    {
        switch (Singletons.Instance.Notescript.noofNotesCollected)
        {
            case 1: //One note collected
                MissionTriggerColliders[0].gameObject.SetActive(true);
                break;
        }
    }

    public void LoadCinematic(int mission_id)
    {
        ImageStarting.SetActive(false);

        switch (mission_id)
        {
            case 1: //Gun game
                Singletons.Instance.AudioScript.GameThemeAS.Stop();
                Singletons.Instance.AudioScript.miniGame2Cutscene.Play();
                //Minimap.SetActive(false);
                MasterCam.SetActive(true);
                playerCam.SetActive(false);
                playerRef.gameObject.SetActive(false);
                Cutscenes[mission_id].Play();
                Cutscene2Props.SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    buildings[i].SetActive(false);
                }
                CutsceneUpdate(mission_id);
                loadWBC = true;
                //Disable the Level so it can't be played again
                MissionTriggerColliders[0].gameObject.SetActive(false);

                //Pass to Replay Manager
                ReplayMission.currentMission = 1;

                break;

            case 2: //Run Game
                //Minimap.SetActive(false);
                MasterCam.SetActive(true);
                playerRef.SetActive(false);
                playerCam.SetActive(false);
                playerRef.GetComponent<NavMeshPlayerMvt>().enabled = false;
                CharacterAudioSource.volume = 0f; //Muting running sounds
                Cinematic3_props.SetActive(true);
                Cutscenes[mission_id].Play();

                CutsceneUpdate(mission_id);
                loadCirculation = true;

                MissionTriggerColliders[1].gameObject.SetActive(false);

                ReplayMission.currentMission = 2;

                break;

            case 3: //TD Game
                TD_Cam.gameObject.SetActive(true);
                playerCam.SetActive(false);
                playerRef.SetActive(false);

                MissionTriggerColliders[2].gameObject.SetActive(false);

                TDHolder.SetActive(true);

                foreach (GameObject worldRoad in RoadsWorld)
                {
                    worldRoad.gameObject.SetActive(false);
                }

                foreach (GameObject bench in ConflictBench)
                {
                    bench.SetActive(false);
                }
                //Turn on Population Handler, already done

                ReplayMission.currentMission = 3;
                quesScript.enabled = true;
                quesScript.GenerateRandomQues();

                break;
            case 4:
                //Minimap.SetActive(false);
                playerCam.SetActive(false);
                //playerRef.GetComponent<NavMeshPlayerMvt>().enabled = false;
                MasterCam.SetActive(true);
                MissionTriggerColliders[3].gameObject.SetActive(false);
                cinematic4Props.SetActive(true);
                Boundary.SetActive(false);
                Cutscenes[3].Play();

                
                break;

        }
    }

    public void OnPlay()
    {
        play = true;
    }
        
}
