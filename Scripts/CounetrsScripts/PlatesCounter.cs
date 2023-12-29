using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO platekitchenobjectS0;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int PlatesAmountToSpawn;
    private int PlatesAmountToSpawnMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (KitchenGameManager.Instance.isGamePlaying() && PlatesAmountToSpawn < PlatesAmountToSpawnMax)
            {
                PlatesAmountToSpawn++;

                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }

        }
     
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if(PlatesAmountToSpawn > 0)
            {
                PlatesAmountToSpawn--;
                KitchenObject.SpawnKitchenObject(platekitchenobjectS0, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
           
        }
    }
}
