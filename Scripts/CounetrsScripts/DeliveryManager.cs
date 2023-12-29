using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    private int successfulrecepiesAmount;
    public static DeliveryManager Instance { get; private set; }


    [SerializeField] private RecepySOList RecepySOList;
    private List<RecipySO> WaitingRecepySOlist;
    private float spawnRecepyTimer;
    private float spawnRecepyTimerMax = 4f;
    private int WaitingRecepyMax = 4;
    private void Awake()
    {
        Instance = this;
        WaitingRecepySOlist = new List<RecipySO>();
    }

    private void Update()
    {
        spawnRecepyTimer -= Time.deltaTime;
        if (spawnRecepyTimer <= 0f)
        {
            spawnRecepyTimer = spawnRecepyTimerMax;
            if (KitchenGameManager.Instance.isGamePlaying()  && WaitingRecepySOlist.Count < WaitingRecepyMax)
            {
                RecipySO watingRecepySO = RecepySOList.RecepySOLists[UnityEngine.Random.Range(0, RecepySOList.RecepySOLists.Count)];
            
                WaitingRecepySOlist.Add(watingRecepySO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

            }
        }
    }
    public void DeliverRecepy(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < WaitingRecepySOlist.Count; i++)
        {
            RecipySO waitingRecepySO = WaitingRecepySOlist[i];

            if (waitingRecepySO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // has same no of ingredients
                bool platecontentMatchesRecepy = true;

                foreach (KitchenObjectSO RecepykitchenObjectSO in waitingRecepySO.kitchenObjectSOList)
                {
                    // cycling through all ingredients in recepy
                    bool ingredientfound = false;
                    foreach (KitchenObjectSO platekitchenobjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // cycling through all ingredients in plate
                        if (platekitchenobjectSO == RecepykitchenObjectSO)
                        {
                            // ingredients matches
                            ingredientfound = true;
                            break;
                        }
                    }
                    if (!ingredientfound)
                    {
                        // this recepy was not ound on the plate
                        platecontentMatchesRecepy = false;
                    }
                }
                if (platecontentMatchesRecepy)
                {
                    // player delivered correct recepy
                    successfulrecepiesAmount++;


                    WaitingRecepySOlist.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }
        // no matches found
        // player did not deliver correct recepy
        OnRecipeFail?.Invoke(this, EventArgs.Empty);

    }

    public List<RecipySO> GetWaitingRecipeSOList()
    {
        return WaitingRecepySOlist;
    }
    public int getsuccessfulrecepiesAmount()
    {
        return successfulrecepiesAmount;
    }
}
