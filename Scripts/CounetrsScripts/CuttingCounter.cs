using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.onprogresschangedeventargs> OnProgressChnaged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;

   new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    [SerializeField] private CuttingRecipySO[] CuttingRecipySOArray;

    public int cuttingprogress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // No KitchenObject  On Counter 
            if (player.HasKitchenObject())
            {
                if (HasRecepieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetkitchenObjectParrent(this);
                    cuttingprogress = 0;

                    CuttingRecipySO cuttingRecipySO = GetcuttingRecepieSOwithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChnaged?.Invoke(this,new IHasProgress.onprogresschangedeventargs{
                        progressNormalised = (float)cuttingprogress / cuttingRecipySO.CuttingProgressMax
                    });
                }

            }
            else
            {
                // not carrying anything
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
            }
            else
            {
                // Player is not carrying object
                GetKitchenObject().SetkitchenObjectParrent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecepieWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {    // there is kitchenObjct on counter N  IT CAN BE CUTTED
            cuttingprogress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipySO cuttingRecipySO = GetcuttingRecepieSOwithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
            {
                progressNormalised = (float)cuttingprogress / cuttingRecipySO.CuttingProgressMax
            });
            if (cuttingprogress >= cuttingRecipySO.CuttingProgressMax)
            {
                KitchenObjectSO outputkitchenObjSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputkitchenObjSO, this);
            }

         
        }
    }


    private  bool  HasRecepieWithInput(KitchenObjectSO inputKitchenObj)
    {
        CuttingRecipySO cuttingRecipySO = GetcuttingRecepieSOwithInput(inputKitchenObj);
       
            return cuttingRecipySO!=null;
        
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObject)
    {
        CuttingRecipySO cuttingRecipySO = GetcuttingRecepieSOwithInput(inputkitchenObject);
        if (cuttingRecipySO != null)
        {
            return cuttingRecipySO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipySO GetcuttingRecepieSOwithInput(KitchenObjectSO inputobjSO)
    {
        foreach (CuttingRecipySO cuttingRecipySO in CuttingRecipySOArray)
        {
            if (cuttingRecipySO.input == inputobjSO)
            {
                return cuttingRecipySO;
            }
        }
        return null;
    }
}
