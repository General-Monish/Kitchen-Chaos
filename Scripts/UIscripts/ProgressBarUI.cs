using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] private GameObject HasProgressGameObject;
    [SerializeField] private Image Bar;
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = HasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Game Object" + HasProgressGameObject + "does not have a component that impliemmnts IHasProgress");
        }
        hasProgress.OnProgressChnaged += HasProgress_OnProgressChnaged;
        Bar.fillAmount = 0f;

        Hide();
    }

    private void HasProgress_OnProgressChnaged(object sender, IHasProgress.onprogresschangedeventargs e)
    {
        Bar.fillAmount = e.progressNormalised;

        if (e.progressNormalised == 0f || e.progressNormalised == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
