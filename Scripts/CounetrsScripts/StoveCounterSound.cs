using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    private AudioSource AudioSource;
    private bool StoveCounterBurningWarningSound;
    private float wrningSoundTimer;

    public void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StoveCounter.OnstateChanged += StoveCounter_OnstateChanged;
        StoveCounter.OnProgressChnaged += StoveCounter_OnProgressChnaged;
    }

    private void StoveCounter_OnProgressChnaged(object sender, IHasProgress.onprogresschangedeventargs e)
    {
        float burnShowProgressAmount = 0.5f;
         StoveCounterBurningWarningSound = StoveCounter.isFried() && e.progressNormalised >= burnShowProgressAmount;
    }

    private void StoveCounter_OnstateChanged(object sender, StoveCounter.OnstateEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            AudioSource.Play();
        }
        else
        {
            AudioSource.Pause();
        }
    }

    private void Update()
    {
        if (StoveCounterBurningWarningSound)
        {
            wrningSoundTimer -= Time.deltaTime;
            if (wrningSoundTimer <= 0f)
            {
                float warningsoundtimerMax = 0.2f;
                wrningSoundTimer = warningsoundtimerMax;

                SoundManager.Instance.PLayWarningSound(StoveCounter.transform.position);
            }
        }

    }
}
