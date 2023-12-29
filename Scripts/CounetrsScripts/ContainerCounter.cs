using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter

    
{

    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjSO;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player isnt carrying anything


            KitchenObject.SpawnKitchenObject(kitchenObjSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
