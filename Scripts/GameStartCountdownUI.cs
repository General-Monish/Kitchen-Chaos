using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private Animator anim;
    private int PreviousCountDownNumber;
    private const string POPUP = "POPUP";
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        KitchenGameManager.Instance.Onstatechang += KitchenGameManager_Onstatechang;
    }

    private void KitchenGameManager_Onstatechang(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.isGameCountdownToStart())
        {
            show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetGameCountdownTStartTimer());
        countdownText.text = countdownNumber.ToString(); // decimals gayab (mathf.ceil)

        if (PreviousCountDownNumber != countdownNumber)
        {
            PreviousCountDownNumber = countdownNumber;
            anim.SetTrigger(POPUP);
            SoundManager.Instance.CountDownSoundSound();
        }
    }

    private void show()
    {
        gameObject.SetActive(true);
    }  private void Hide()
    {
        gameObject.SetActive(false);
    }


}
