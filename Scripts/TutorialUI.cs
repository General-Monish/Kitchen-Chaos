using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upkeytext;
    [SerializeField] private TextMeshProUGUI downkeytext;
    [SerializeField] private TextMeshProUGUI leftkeytext;
    [SerializeField] private TextMeshProUGUI rightkeytext;
    [SerializeField] private TextMeshProUGUI interacttext;
    [SerializeField] private TextMeshProUGUI Alttext;
    [SerializeField] private TextMeshProUGUI pausekeytext;
    [SerializeField] private TextMeshProUGUI gamepadkeyinteracttext;
    [SerializeField] private TextMeshProUGUI gamepadalttext;
    [SerializeField] private TextMeshProUGUI gamepadpausetext;
    [SerializeField] private TextMeshProUGUI gamepadLeftKeytext;

    private void Update()
    {
        GameInput.Instance.OnBindingRebind += Gameinput_OnBindingRebind;
        KitchenGameManager.Instance.Onstatechang += KitchenGameManager_Onstatechang;
        UpdatVisual();
        show();
    }

    private void KitchenGameManager_Onstatechang(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.isGameCountdownToStart())
        {
            hide();
        }
    }

    private void Gameinput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdatVisual();
    }

    private void UpdatVisual()
    {
        upkeytext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        downkeytext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        leftkeytext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        rightkeytext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interacttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        Alttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.interactAlt);
        pausekeytext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadkeyinteracttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadalttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadpausetext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
       
    }

    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }

}
