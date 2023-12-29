using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IkitchenObjectParrent kitchenObjectparrent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return  kitchenObjectSO;
    }

    public void SetkitchenObjectParrent(IkitchenObjectParrent kitchenobjectparrent )
    {
        if (this.kitchenObjectparrent != null)
        {
            this.kitchenObjectparrent.ClearKitchenObject();
        }
        this.kitchenObjectparrent = kitchenobjectparrent;

        if (kitchenObjectparrent.HasKitchenObject())
        {
            Debug.LogError("ikitchenobjectparrent Already Has Kitchen Object");
        }

        kitchenObjectparrent.SetKitchenObject(this);
        transform.parent = kitchenObjectparrent.GetKitchenObejectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IkitchenObjectParrent GetKitchenObjectparrent()
    {
        return kitchenObjectparrent;
    }

    public void DestroySelf()
    {
        kitchenObjectparrent.ClearKitchenObject();
        Destroy(gameObject);
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IkitchenObjectParrent kitchenObjectParrent)
    {
        Transform kitchenobjectTransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObject kitchenObject = kitchenobjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetkitchenObjectParrent(kitchenObjectParrent);

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
