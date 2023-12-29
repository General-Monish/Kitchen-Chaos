using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private static string Player_Music_Prefebs = "Music Prefabs";
    public static MusicManager Instance { get; private set; }
    private float volume=.3f;
    private AudioSource AudioSource;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(Player_Music_Prefebs, .3f);
        AudioSource = GetComponent<AudioSource>();
    }
    public void ChangeVol()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        AudioSource.volume = volume;
        PlayerPrefs.SetFloat(Player_Music_Prefebs, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
