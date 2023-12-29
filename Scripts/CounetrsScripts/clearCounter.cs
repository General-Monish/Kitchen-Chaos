using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjSO;
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            // No KitchenObject  On Counter 
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetkitchenObjectParrent(this);
            }
        }
        else
        {
            // Having KitchenObject On Counter
            if (player.HasKitchenObject())
            {
                // Player is Carrying The object Now which was Placed at counter
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))// here we are trying to get plate first then adding ingredients
                {
                    // player is holding a plate 
               
                    if (plateKitchenObject.TryAddingredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                 
                    // player is not carrying plate but something else)
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // counter is holding a plate
                        if (plateKitchenObject.TryAddingredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying object
                GetKitchenObject().SetkitchenObjectParrent(player);
            }
        }
    }

   
}
