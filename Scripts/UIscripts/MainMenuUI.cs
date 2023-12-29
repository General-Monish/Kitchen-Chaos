using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    public void Awake()
    {
        PlayButton.onClick.AddListener(() =>
        {
            LoaderUI.load(LoaderUI.Scene.Game);
        }); 
        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }


}
