using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IkitchenObjectParrent

{

    public static Player Instance { get; private set; }
    public  event EventHandler OnPickingUp;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float speed=10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask CounterLayerMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one player instance");
        }
        Instance = this;
    }

    private bool iswalk;
    private Vector3 LastInteractDist;
    private BaseCounter selectedcounter;
    private KitchenObject kitchenobject;
    private void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction;
        gameInput.OnInteractionAlternate += GameInput_OnInteractionAlternate;
    }

    private void GameInput_OnInteractionAlternate(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.isGamePlaying()) return;
        if (selectedcounter != null)
        {
            selectedcounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.isGamePlaying()) return;
        if (selectedcounter != null)
        {
            selectedcounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalk()
    {
        return iswalk;
    }

   private void HandleInteractions()
{
    Vector2 inputVector = gameInput.GameVectorInputNormalized();
    Vector3 moveddir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveddir != Vector3.zero)
        {
            LastInteractDist = moveddir;
        }
    float InteractDist = 1f;

    // Draw the raycast for visualization (this line will draw a red line in the Scene view)
    Debug.DrawRay(transform.position, moveddir*InteractDist, Color.red);

    if (Physics.Raycast(transform.position, LastInteractDist, out RaycastHit raycastHit, InteractDist,CounterLayerMask))
    {

            // instead of using tags we used this method of identifying object
      if(raycastHit.transform.TryGetComponent(out BaseCounter basecounter))
            {
                // has clear counter
                if (basecounter != selectedcounter)
                {
                   
                    SetSelectedCounter(basecounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
    }
        else
        {
            SetSelectedCounter(null);
        }
    }


    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GameVectorInputNormalized();

        Vector3 moveddir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = speed * Time.deltaTime;
     

        float playerheight = 2f;
        float playerRadius = .7f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveddir, moveDistance);

        if (!canMove)
        {
            // attempt only x dir
            Vector3 movedirX = new Vector3(moveddir.x, 0, 0).normalized;
            canMove = (moveddir.x<-.5f ||moveddir.x>+.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, movedirX, moveDistance);

            if (canMove)
            {
                moveddir = movedirX;
            }
            else
            {
                // cant move only on x

                // attempt only z movement

                Vector3 moveDirZ = new Vector3(0, 0, moveddir.z).normalized;
                canMove = (moveddir.z < -.5f || moveddir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveddir = moveDirZ;
                }
                else
                {
                    // cant move in any dir
                }
            }
        }
        if (canMove)
        {
            transform.position += moveddir * speed * Time.deltaTime;
        }

        iswalk = moveddir != Vector3.zero; // idle state 
        float rotatespeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveddir, Time.deltaTime * rotatespeed);
    }


    private void SetSelectedCounter(BaseCounter selectedcounter)
    {
        this.selectedcounter = selectedcounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedcounter
        });
    }

    public Transform GetKitchenObejectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenobject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickingUp?.Invoke(this, EventArgs.Empty);
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
