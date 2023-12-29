using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GAmeObject 
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    [SerializeField] private PlateKitchenObject PlateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GAmeObject> kitchenObjectSO_GAmeObjectsList;

    private void Start()
    {
        PlateKitchenObject.oningredientAdded += PlateKitchenObject_oningredientAdded;
        foreach (KitchenObjectSO_GAmeObject kitchenObjectSO_GAmeObject in kitchenObjectSO_GAmeObjectsList)
        {
            
                kitchenObjectSO_GAmeObject.GameObject.SetActive(false);
            
        }
    }

    private void PlateKitchenObject_oningredientAdded(object sender, PlateKitchenObject.oningredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GAmeObject kitchenObjectSO_GAmeObject in kitchenObjectSO_GAmeObjectsList)
        {
            if (kitchenObjectSO_GAmeObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSO_GAmeObject.GameObject.SetActive(true);
            }
        }
    }
}
