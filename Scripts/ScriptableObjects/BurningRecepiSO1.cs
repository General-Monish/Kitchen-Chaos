using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class BurningRecepiSO1 : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int BurningTimerMax;
}
