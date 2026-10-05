using System;
using UnityEngine;

public class MouseInputManager
{

    private PlayerActions _pActions;

    private MouseInputManager()
    {
        _pActions = new PlayerActions();
    }
    private static MouseInputManager _mInputManager;

    public static MouseInputManager mouseInput
    {
        get
        {
            if(_mInputManager == null)
            {
                _mInputManager = new MouseInputManager();
            }
            return _mInputManager;
        }
    }

    private void EnableMouseInput()
    {
        //_pActions.PlayerIso.MoveCommand.Enable();
    }


    // 3 Different Mouse Input types
    // Move, Interact, and UI
    // Move command moves selected to worldspace position
    // Interact moves to point within interact range and performs action based on interact command
    // UI interact for button presses (ignored/delegated to UI system)




    
}
