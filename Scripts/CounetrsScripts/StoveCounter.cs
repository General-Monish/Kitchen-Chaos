using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress

{
    public event EventHandler<IHasProgress.onprogresschangedeventargs> OnProgressChnaged;
    public event EventHandler<OnstateEventArgs> OnstateChanged;
    public class OnstateEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecepiSO[]  FryingRecepiSOAraay; 
    [SerializeField] private BurningRecepiSO1[] BurningRecepiSOAraay;

    private State state;
    private float FryingTimer;
    private FryingRecepiSO fryingRecepiSO;
    private float BurningTimer;
    private BurningRecepiSO1 burningRecepiSO;


    private void Start()
    {
        state=State.idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {

            switch (state)
            {
                case State.idle:
                    break;
                case State.Frying:
                    FryingTimer += Time.deltaTime;
                    OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
                    {
                        progressNormalised = FryingTimer / fryingRecepiSO.FryingTimerMax
                    });
                    if (FryingTimer > fryingRecepiSO.FryingTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecepiSO.output, this);
                     
                        state = State.Fried;
                        BurningTimer = 0f;
                        burningRecepiSO = GetBurningRecepieSOwithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnstateChanged?.Invoke(this, new OnstateEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                      BurningTimer += Time.deltaTime;
                    OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
                    {
                        progressNormalised = BurningTimer / burningRecepiSO.BurningTimerMax
                    });
                    if (BurningTimer > burningRecepiSO.BurningTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecepiSO.output, this);
                        Debug.Log("Object Burned");
                        state = State.Burned;
                        OnstateChanged?.Invoke(this, new OnstateEventArgs
                        {
                            state = state
                        });
                        OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
                        {
                            progressNormalised = 0f
                        }) ;
                    }
                    break;
                case State.Burned:
                    break;
            }
        
        }

    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
       
            // No KitchenObject  On Counter 
            if (player.HasKitchenObject())
            {
                // player is carrying something
                if (HasRecepieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // player carrying something that can be fried
                    player.GetKitchenObject().SetkitchenObjectParrent(this);

                   fryingRecepiSO = GetfryingRecepieSOwithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    FryingTimer = 0f;
                    OnstateChanged?.Invoke(this, new OnstateEventArgs
                    {
                        state = state
                    });
                    OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
                    {
                        progressNormalised = FryingTimer / fryingRecepiSO.FryingTimerMax
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
                        state = State.idle;
                        OnstateChanged?.Invoke(this, new OnstateEventArgs
                        {
                            state = state
                        });
                        OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
                        {
                            progressNormalised = 0f
                        });
                    }
                }
            }
            else
            {
                // Player is not carrying object
                GetKitchenObject().SetkitchenObjectParrent(player);
                state = State.idle;
                OnstateChanged?.Invoke(this, new OnstateEventArgs
                {
                    state = state
                });
                OnProgressChnaged?.Invoke(this, new IHasProgress.onprogresschangedeventargs
                {
                    progressNormalised = 0f
                });
            }
        }
    }

    private bool HasRecepieWithInput(KitchenObjectSO inputKitchenObj)
    {
        FryingRecepiSO FryingRecipySO = GetfryingRecepieSOwithInput(inputKitchenObj);

        return FryingRecipySO != null;

    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObject)
    {
        FryingRecepiSO FryingRecipySO = GetfryingRecepieSOwithInput(inputkitchenObject);
        if (FryingRecipySO != null)
        {
            return FryingRecipySO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecepiSO GetfryingRecepieSOwithInput(KitchenObjectSO inputobjSO)
    {
        foreach (FryingRecepiSO fryingRecipySO in FryingRecepiSOAraay)
        {
            if (fryingRecipySO.input == inputobjSO)
            {
                return fryingRecipySO;
            }
        }
        return null;
    }

    private BurningRecepiSO1 GetBurningRecepieSOwithInput(KitchenObjectSO inputobjSO)
    {
        foreach (BurningRecepiSO1 burningRecepiSO in BurningRecepiSOAraay)
        {
            if (burningRecepiSO.input == inputobjSO)
            {
                return burningRecepiSO;
            }
        }
        return null;
    }

    public bool isFried()
    {
        return state == State.Fried;
    }
}
