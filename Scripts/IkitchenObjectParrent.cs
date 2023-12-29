using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IkitchenObjectParrent 
{
  

    public Transform GetKitchenObejectFollowTransform();


    public void SetKitchenObject(KitchenObject kitchenObject);


    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();


    public bool HasKitchenObject();
  
}
