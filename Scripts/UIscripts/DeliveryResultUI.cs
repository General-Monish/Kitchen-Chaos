using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string pop = "pop";
    [SerializeField] private Image backgroundimage;
    [SerializeField] private Image Iconimage;
    [SerializeField] private TextMeshProUGUI mesgText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color FailedColor;
    [SerializeField] private Sprite SuccessSprite;
    [SerializeField] private Sprite failedSprite;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += dELIVERYmANAGER_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        anim.SetTrigger(pop);
        backgroundimage.color = FailedColor;
        Iconimage.sprite = failedSprite;
        mesgText.text = "Deliver\nFailed";
    }

    private void dELIVERYmANAGER_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        anim.SetTrigger(pop);
        backgroundimage.color = successColor;
        Iconimage.sprite = SuccessSprite;
        mesgText.text = "Success\nFailed";
    }
}
