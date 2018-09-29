using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void VolumeChangerDelegate(AudioSource source);
public class AudioManager : MonoBehaviour
{
    //lists containing sounds 
    private static List<AudioClip> SFXClips = new List<AudioClip>();
    private static List<AudioClip> MusicClips = new List<AudioClip>();
    private static VolumeChangerDelegate soundDelegate;
    public static AudioSource MusicSource;
    public static AudioSource SFXSource;
     
    //list of sounds and their id

    /* Sound Effect ID list (SFX)
    0:broot
    1:gaspickup
    2:steve
    3:water
     */

    /* Music ID list
     0: Ambient Music
     */

    /// <summary>
    /// Randomizes the pitch of a clip
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="source"></param>
    private static void RandomizeSFX(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }

    private static void SFXMethod(AudioSource source, int id)
    {
        if(source.isPlaying)
        {
            soundDelegate(source);
        }
        RandomizeSFX(SFXClips[id], source);
    }

    private void MusicVolumeChanger(AudioSource source)
    {
        StartCoroutine(AnimateMusicIn(1.0f, source));
    }

    private static void MusicMethod(AudioSource source, int id)
    {
        if(source.isPlaying)
        {
            soundDelegate(source);
        }

        source.clip = MusicClips[id];
        source.Play();
    }

    private static void StopSound(AudioSource source, int id)
    {
        source.Stop();
    }

    IEnumerator AnimateMusicIn(float duration, AudioSource source)
    {
        float percent = 0;

        while(percent < 0)
        {

            percent += Time.deltaTime * 1 / duration;

            source.volume = Mathf.Lerp(0, source.volume, percent);

            yield return null;
        }
    }

    //Unity Methods

    private void Awake()
    {
        for(int i = 0; i < GetComponentsInChildren<AudioSource>().Length; i++)
        {
            if(i == 0)
            {
                SFXSource = GetComponentsInChildren<AudioSource>()[i];
            }
            else
            {
                MusicSource = GetComponentsInChildren<AudioSource>()[i];
            }
        }

        SFXClips.AddRange(Resources.LoadAll<AudioClip>(""));

        soundDelegate += MusicVolumeChanger;
    }

    private void Start()
    {
        //sound effect handler adding
        EventManager.SoundAddHandler(EVENT.PlaySFX, SFXMethod);

        //music handler adding
        EventManager.SoundAddHandler(EVENT.PlayMusic, MusicMethod);
    }
}