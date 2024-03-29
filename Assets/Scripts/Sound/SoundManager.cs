using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO _audioClipRefsSo;

    [SerializeField] private float volumeBegin = .3f;
    [SerializeField] private float volumeUp = .1f;
    private float volume;
    private void Awake()
    {
        Instance = this;
        volume = volumeBegin;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlaceHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipRefsSo.ObjectDrop, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(_audioClipRefsSo.ObjectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(_audioClipRefsSo.ObjectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipRefsSo.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryManager = DeliveryCounter.Instance;
        PlaySound(_audioClipRefsSo.DeliveryFail, deliveryManager.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryManager = DeliveryCounter.Instance;
        PlaySound(_audioClipRefsSo.DeliverySuccess, deliveryManager.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArr, Vector3 Position)
    {
        PlaySound(audioClipArr[Random.Range(0, audioClipArr.Length)], Position);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 Position)
    {
        AudioSource.PlayClipAtPoint(audioClip, Position, volume);
    }

    public void PlayFootstepsSound(Vector3 position)
    {
        PlaySound(_audioClipRefsSo.FootStep, position);
    }

    public void PlayCountDownSound()
    {
        PlaySound(_audioClipRefsSo.Warning, Vector3.zero);
    }
    public void ChangeSoundVolume()
    {
        volume += volumeUp;
        if (volume > 1f)
        {
            volume = 0f;
        }
    }

    public float GetVolumeSound()
    {
        return volume;
    }
}
