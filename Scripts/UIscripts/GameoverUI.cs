using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecepiesDeliveredtext;

    private void Start()
    {
        KitchenGameManager.Instance.Onstatechang += KitchenGameManager_Onstatechang;
        Hide();
    }

    private void KitchenGameManager_Onstatechang(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            show();
            RecepiesDeliveredtext.text = DeliveryManager.Instance.getsuccessfulrecepiesAmount().ToString();
        }
        else
        {
            Hide();
        }
    }


    private void show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
