using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button SoundEffectButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveleftButton;
    [SerializeField] private Button moverightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton; 
    [SerializeField] private Button GamepadInteractButton;
    [SerializeField] private Button GamepadInteractAltButton;
    [SerializeField] private Button GamepadPauseButton;
    [SerializeField] private TextMeshProUGUI SoundEffectText;
    [SerializeField] private TextMeshProUGUI Musictext;
    [SerializeField] private TextMeshProUGUI uptext;
    [SerializeField] private TextMeshProUGUI downtext;
    [SerializeField] private TextMeshProUGUI lefttext;
    [SerializeField] private TextMeshProUGUI righttext;
    [SerializeField] private TextMeshProUGUI interacttext;
    [SerializeField] private TextMeshProUGUI interactAlttext;
    [SerializeField] private TextMeshProUGUI Pausetext;  
    [SerializeField] private TextMeshProUGUI Gamepadinteracttext;
    [SerializeField] private TextMeshProUGUI GamepadinteractAlttext;
    [SerializeField] private TextMeshProUGUI GamepadPausetext;
    [SerializeField] private Transform PressKeyToRebindTransform;

    private Action onclosebuttonaction;

    private void Awake()
    {
        Instance = this;
        SoundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVol();
            UpdatVisual();
        });
        MusicButton.onClick.AddListener(() => 
        {
            MusicManager.Instance.ChangeVol();
            UpdatVisual();
        });
        CloseButton.onClick.AddListener(() =>
        {

            hide();
            onclosebuttonaction();
        });
        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveleftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moverightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        InteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        InteractAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.interactAlt); });
        PauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); }); 
        GamepadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Interact); });
        GamepadInteractAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_InteractAlternate); });
        GamepadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Pause); });

    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePause += KitchenGaameManager_OnGamePause;
        UpdatVisual();
        hidePressKeyToRebind();
        hide();
    }

    private void KitchenGaameManager_OnGamePause(object sender, System.EventArgs e)
    {
        hide();
    }

    private void UpdatVisual()
    {
        SoundEffectText.text = "Sound Effect" + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        Musictext.text = "Music" + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        uptext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        downtext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        lefttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        righttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interacttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.interactAlt);
        Pausetext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);  
        Gamepadinteracttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        GamepadinteractAlttext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        GamepadPausetext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void show(Action onclosebuttonAction)
    {
        this.onclosebuttonaction = onclosebuttonAction;
        gameObject.SetActive(true);

        SoundEffectButton.Select();
    }
    private  void hide()
    {
        gameObject.SetActive(false);
    }
    private void showPressKeyToRebind()
    {
        PressKeyToRebindTransform.gameObject.SetActive(true);
    }
    private void hidePressKeyToRebind()
    {
        PressKeyToRebindTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        showPressKeyToRebind();
        GameInput.Instance.Rebindbinding(binding , ()=> {
            hidePressKeyToRebind();
            UpdatVisual();
            });
    }
}
