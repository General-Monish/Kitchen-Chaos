using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform PlateVisualPrefab;

    private List<GameObject> platevisualgameobjList;

    private void Awake()
    {
        platevisualgameobjList = new List<GameObject>();
    }


    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plategameobject = platevisualgameobjList[platevisualgameobjList.Count - 1];
        platevisualgameobjList.Remove(plategameobject);
        Destroy(plategameobject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender,System.EventArgs e)
    {
        Transform platevisualTransform = Instantiate(PlateVisualPrefab, counterTopPoint);
        float plateofsetY = 0.1f;

        platevisualTransform.localPosition = new Vector3(0, plateofsetY*platevisualgameobjList.Count, 0);
        platevisualgameobjList.Add(platevisualTransform.gameObject);
    }

  
   
}
