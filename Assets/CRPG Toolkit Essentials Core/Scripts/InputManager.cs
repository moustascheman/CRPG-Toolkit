using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace CRPGToolkit.Core
{



    /// <summary>
    /// Class used as an interface for the input actions.
    /// Sends events that controllers can subscribe to for non-ui related inputs.
    /// </summary>
    public class InputManager
    {
        private PlayerActions _pActions;
        private bool _dragging = false;

        public bool _isEnabled = true;

        public event EventHandler? GameplayMouseClicked;

        public event EventHandler<Rect> SelectionDragMouseEnded;


        private InputManager()
        {
            _pActions = new PlayerActions();
        }

        private Vector2 dragMouseStartPos;

        private static InputManager _instance;

        public static InputManager mouseManager
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                    _instance.SetupMouseManager();
                }
                return _instance;
            }
        }

        private void SetupMouseManager()
        {
            //_pActions.PlayerIso.DragCommand.Enable();
            //_pActions.PlayerIso.DragCommand.performed += OnDragStart;
            //_pActions.PlayerIso.DragCommand.canceled += OnDragEnd;

            _pActions.PlayerIso.Click.Enable();
            _pActions.PlayerIso.Click.canceled += HandleMouseClick;
          //  _pActions.PlayerIso.MoveCommand.canceled += DebugMouseEnd;


        }


        private void OnMouseClickStart()
        {
            
        }


        private void OnMouseClickEnd()
        {
            
        }

        private void DebugMouseStart(InputAction.CallbackContext ctx)
        {
            Debug.Log("START");
        }

        private void DebugMouseEnd(InputAction.CallbackContext ctx)
        {
            Debug.Log("End");
        }


        private void HandleMouseClick(InputAction.CallbackContext context)
        {

            if (_isEnabled && !_dragging)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Currently clicking on UI, don't do anything
                    return;
                }

                GameplayMouseClicked.Invoke(this, null);
            }
        }


        private void OnDragStart(InputAction.CallbackContext ctx)
        {
            if (_isEnabled)
            {
                dragMouseStartPos = Vector2.zero;
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Currently clicking on UI, don't do anything
                    return;
                }
                dragMouseStartPos = Mouse.current.position.ReadValue();
                _dragging = true;
            }
        }

        private void OnDragEnd(InputAction.CallbackContext ctx)
        {
            if (_isEnabled && dragMouseStartPos != Vector2.zero && _dragging)
            {
                Vector2 currentMousePos = Mouse.current.position.ReadValue();
                float width = currentMousePos.x - dragMouseStartPos.x;
                float height = currentMousePos.y - dragMouseStartPos.y;
                Rect box = new Rect(dragMouseStartPos.x, (Screen.height - currentMousePos.y), width, height);
                Debug.Log(box);

            }
            _dragging = false;

        }



    }
}