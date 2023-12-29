using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter StoveCounter;
    [SerializeField] private GameObject StoveOnGameobject;
    [SerializeField] private GameObject ParticleGameobject;

    private void Start()
    {
        StoveCounter.OnstateChanged += StoveCounter_OnstateChanged;
    }

    private void StoveCounter_OnstateChanged(object sender, StoveCounter.OnstateEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        StoveOnGameobject.SetActive(showVisual);
        ParticleGameobject.SetActive(showVisual);
    }
}
