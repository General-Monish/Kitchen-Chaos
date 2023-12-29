using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject PlateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        PlateKitchenObject.oningredientAdded += PlateKitchenObject_oningredientAdded;
    }

    private void PlateKitchenObject_oningredientAdded(object sender, PlateKitchenObject.oningredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in PlateKitchenObject.GetKitchenObjectSOList())
        {
            Transform icontransform = Instantiate(iconTemplate, transform);
            icontransform.gameObject.SetActive(true);
            icontransform.GetComponent<PlateIconSingleUI>().setkitchenObjectSO(kitchenObjectSO);
        }
    }
}
