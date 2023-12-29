using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SttoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;

    private void Start()
    {
        StoveCounter.OnProgressChnaged += StoveCounter_OnProgressChnaged;
        Hide();
    }

    private void StoveCounter_OnProgressChnaged(object sender, IHasProgress.onprogresschangedeventargs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = StoveCounter.isFried() && e.progressNormalised >= burnShowProgressAmount;
        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    } 
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
