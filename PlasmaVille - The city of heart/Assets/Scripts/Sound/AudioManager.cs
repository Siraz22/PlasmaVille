using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Game Theme Audio")]
    public AudioSource GameThemeAS;

    public AudioClip GameThemeAC;

    public AudioSource miniGame2Cutscene;
    public AudioSource miniGame2Gameplay;

    [Header("Page Curl Audio")]
    public AudioSource PageCurlAS;

    public AudioClip LeftPageAC;
    public AudioClip RightPageAC;


    [Header("Character AS")]
    public AudioSource CharacterAS;

    public AudioClip RunningAC;
}
