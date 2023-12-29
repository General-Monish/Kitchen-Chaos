using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ReciprNamwText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipySO recipySO)
    {
        ReciprNamwText.text = recipySO.recepyName;


        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipySO.kitchenObjectSOList)
        {
            Transform icontransform = Instantiate(iconTemplate, iconContainer);
            icontransform.gameObject.SetActive(true);
            icontransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }


}
