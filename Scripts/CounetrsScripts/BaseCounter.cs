using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IkitchenObjectParrent
{

    public static event EventHandler OnAnyObjDropHere;

    public static void ResetStaticData()
    {
        OnAnyObjDropHere = null;
    }
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenobject;
    public virtual void Interact(Player player)
    {
        Debug.LogError("basecounter.Interact");
    }

    public virtual void InteractAlternate(Player player)
    {
      //  Debug.LogError("basecounter.InteractAlternate();");
    }

    public Transform GetKitchenObejectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenobject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyObjDropHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenobject;
    }

    public void ClearKitchenObject()
    {
        kitchenobject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenobject != null;
    }
}