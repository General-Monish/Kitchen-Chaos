using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<oningredientAddedEventArgs> oningredientAdded;
  public class oningredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [SerializeField] List<KitchenObjectSO> kitchenObjectSOsListValid;
    // Start is called before the first frame update
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddingredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!kitchenObjectSOsListValid.Contains(kitchenObjectSO))
        {
            // not a valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Already has this type 
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            oningredientAdded?.Invoke(this, new oningredientAddedEventArgs
            {
                KitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        


    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }

}
