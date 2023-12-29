using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREF_SOUND_EFFECT = "Sound Effects";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipReferanceSO AudioClipReferanceSO;
    private float volume=1f;
    private void Awake()
    {
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_EFFECT, 1f);
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickingUp += Player_OnPickingUp;
        BaseCounter.OnAnyObjDropHere += BaseCounter_OnAnyObjDropHere;
        TrashCounter.OnAnyObjTrashed += TrashCounter_OnAnyObjTrashed;
    }

    private void TrashCounter_OnAnyObjTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(AudioClipReferanceSO.TrashWarning, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjDropHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(AudioClipReferanceSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickingUp(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipReferanceSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(AudioClipReferanceSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(AudioClipReferanceSO.deliveryfail,deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(AudioClipReferanceSO.deliverysuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip , Vector3 position, float volumeMultiplier=1f )
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier*volume);
    }

    public void PlayFootStepSound(Vector3 position , float volume)
    {
        PlaySound(AudioClipReferanceSO.footSteps, position, volume);
    }
    public void CountDownSoundSound()
    {
        PlaySound(AudioClipReferanceSO.Warning ,Vector3.zero);
    } 
    public void PLayWarningSound(Vector3 position)
    {
        PlaySound(AudioClipReferanceSO.Warning ,position);
    }
    public void ChangeVol()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_EFFECT, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
         return volume;
    }
}
