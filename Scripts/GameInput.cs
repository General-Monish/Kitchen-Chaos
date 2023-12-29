using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions playerinputActions;
    public event EventHandler OnInteraction;
    public event EventHandler OnInteractionAlternate;
    public event EventHandler OnPauseEventAction;
    public event EventHandler OnBindingRebind;

    private const string Player_Prefs_Bindings = "InputBindings";
   
    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        interactAlt,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause,
    }

    private void Awake()
    {

        Instance = this;
        playerinputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(Player_Prefs_Bindings))
        {
            playerinputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Player_Prefs_Bindings));
        }
        playerinputActions.Player.Enable();

        playerinputActions.Player.Interact.performed += Interact_performed;
        playerinputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerinputActions.Player.Pause.performed += Pause_performed;


      
    }

    private void OnDestroy()
    {
        playerinputActions.Player.Interact.performed -= Interact_performed;
        playerinputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerinputActions.Player.Pause.performed -= Pause_performed;

        playerinputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseEventAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractionAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GameVectorInputNormalized()
    {
        Vector2 inputVector = playerinputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerinputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerinputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerinputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerinputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerinputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.interactAlt:
                return playerinputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerinputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Gamepad_Interact:
                return playerinputActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return playerinputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return playerinputActions.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void Rebindbinding(Binding binding,Action OnActionRebound)
    {
        playerinputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 1;
                break; 
            case Binding.Move_Down:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerinputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.interactAlt:
                inputAction = playerinputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerinputActions.Player.Pause;
                bindingIndex = 0;
                break; 
            case Binding.Gamepad_Interact:
                inputAction = playerinputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_InteractAlternate:
                inputAction = playerinputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                inputAction = playerinputActions.Player.Pause;
                bindingIndex = 1;
                break;
        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
              
                callback.Dispose();
                playerinputActions.Player.Enable();
                OnActionRebound();

                PlayerPrefs.SetString(Player_Prefs_Bindings, playerinputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
        
    }
}
