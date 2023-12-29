using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button MainmenuButton;
    [SerializeField] private Button ResumeButton;
  
    [SerializeField] private Button OptionsButton;

    private void Awake()
    {
        MainmenuButton.onClick.AddListener(() =>
        {
            LoaderUI.load(LoaderUI.Scene.MainMenu);
        });
        ResumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.gamepauseResume();
        });
        OptionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.show(show);
            hide();
        }); 
     
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGamePause += KitchengameManager_OnGamePause;
        KitchenGameManager.Instance.OnGameUnpause += KitchengameManager_OnGameUnpause;
        hide();
    }

    private void KitchengameManager_OnGameUnpause(object sender, System.EventArgs e)
    {
        hide();
    }

    private void KitchengameManager_OnGamePause(object sender, System.EventArgs e)
    {
        show();
    }

    public void show()
    {
        gameObject.SetActive(true);
        ResumeButton.Select();
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
}
