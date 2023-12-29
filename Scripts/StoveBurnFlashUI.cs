using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashUI : MonoBehaviour
{
    private const string flash = "flash";
    [SerializeField] private StoveCounter StoveCounter;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StoveCounter.OnProgressChnaged += StoveCounter_OnProgressChnaged;
        anim.SetBool(flash, false);
    }

    private void StoveCounter_OnProgressChnaged(object sender, IHasProgress.onprogresschangedeventargs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = StoveCounter.isFried() && e.progressNormalised >= burnShowProgressAmount;
        anim.SetBool(flash, show);

    }

   
}
