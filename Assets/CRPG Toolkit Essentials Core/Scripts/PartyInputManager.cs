using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace CRPGToolkit.Core
{

    public class MouseInputEventArgs : EventArgs
    {
        public Vector2 MousePosition;
        public bool IsShiftClick;
    }


    /// <summary>
    /// Class used as an interface for the input actions.
    /// Sends events that controllers can subscribe to for non-ui related inputs.
    /// </summary>
    public class PartyInputManager
    {
        private PlayerActions _pActions;

        public bool _isEnabled = true;

        public event EventHandler<MouseInputEventArgs> GameplayMouseClickStarted;
        public event EventHandler<EventArgs> GameplayMouseClickEnded;


        public PartyInputManager()
        {
            _pActions = new PlayerActions();
            SetupInputManager();
        }

        private void SetupInputManager()
        {
            _pActions.PlayerIso.Click.Enable();
            _pActions.PlayerIso.ShiftClick.Enable();
            _pActions.PlayerIso.MousePos.Enable();



            _pActions.PlayerIso.Click.started += OnMouseClickStart;
            _pActions.PlayerIso.ShiftClick.started += OnShiftClickStart;
            
            _pActions.PlayerIso.Click.canceled += OnMouseClickEnd;
            _pActions.PlayerIso.ShiftClick.canceled += OnMouseClickEnd;



        }


        private void OnMouseClickStart(InputAction.CallbackContext ctx)
        {   
            if (_isEnabled)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Currently clicking on UI, don't do anything
                    return;
                }

                GameplayMouseClickStarted.Invoke(this, new MouseInputEventArgs(){MousePosition = Input.mousePosition, IsShiftClick = false});
            }
        }

        private void OnShiftClickStart(InputAction.CallbackContext ctx)
        {
            if (_isEnabled)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Currently clicking on UI, don't do anything
                    return;
                }

                GameplayMouseClickStarted.Invoke(this, new MouseInputEventArgs(){MousePosition = Input.mousePosition, IsShiftClick = true});
            }
        }


        private void OnMouseClickEnd(InputAction.CallbackContext ctx)
        {
            GameplayMouseClickEnded.Invoke(this, new EventArgs());
            
        }





    }
}