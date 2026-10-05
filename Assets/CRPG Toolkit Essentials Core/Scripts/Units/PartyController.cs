using System;
using System.Collections.Generic;
using CRPGToolkit.Core;
using UnityEngine;

public class PartyController : MonoBehaviour
{






    [SerializeField]
    private List<Unit> SelectedUnits;
    private PartyInputManager _inputManager;



    ///Click and Drag related variables

    [SerializeField]
    private float _minDragTime;

    [SerializeField]
    private float _minDragDistanceX;

    [SerializeField]
    private float _minDragDistanceY;


    [SerializeField]
    private SelectionBoxController _selectionBoxController;



    private bool _clickStarted = false;
    private bool _isShiftClick = false;




    public void Awake()
    {

    }



    public void Start()
    {
        SelectedUnits = new List<Unit>();
        _inputManager = new PartyInputManager();
        _inputManager.GameplayMouseClickStarted += OnClickStart;
        _inputManager.GameplayMouseClickEnded += OnClickEnd;

    }

    private void OnClickStart(object sender, MouseInputEventArgs e)
    {
        _clickStarted = true;
        if (e.IsShiftClick)
        {
            _isShiftClick = true;
        }
        _selectionBoxController.StartMouseClick(e.MousePosition);

    }

    private void OnClickEnd(object sender, EventArgs args)
    {
        if (_clickStarted)
        {

            RaycastHit[] hits = _selectionBoxController.EndMouseClick();
            if (hits != null)
            {
                if (hits.Length > 0)
                {
                    List<Unit> units = GetUnitListFromBoxcast(hits);
                    if (_isShiftClick)
                    {
                        AddUnitsToSelection(units);
                    }
                    else
                    {
                        SelectUnits(units);
                    }
                }
            }
            else
            {
                OnClick(Input.mousePosition);
            }
        }
        _clickStarted = false;
        _isShiftClick = false;
    }


    private List<Unit> GetUnitListFromBoxcast(RaycastHit[] hits)
    {
        List<Unit> units = new List<Unit>();
        foreach (RaycastHit raycastHit in hits)
        {
            if (raycastHit.collider.TryGetComponent(out ClickHitbox clickHitbox))
            {
                if (clickHitbox.Selectable != null)
                {
                    units.Add((Unit)clickHitbox.Selectable);
                }
            }
        }
        return units;

    }

    private void SelectUnits(List<Unit> units)
    {
        DeselectUnits();
        foreach (Unit u in units)
        {
            SelectedUnits.Add(u);
            u.Select();
        }
    }

    private void AddUnitsToSelection(List<Unit> units)
    {
        foreach (Unit u in units)
        {
            if (!SelectedUnits.Contains(u))
            {
                u.Select();
                SelectedUnits.Add(u);
            }
        }
    }

    public void IssueMoveCommand(Vector3 destination)
    {
        foreach (Unit u in SelectedUnits)
        {
            u.MoveToPoint(destination);
        }
    }



    private void OnClick(Vector2 mousePos)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Vector3 point = hit.point;

            if (hit.collider.TryGetComponent(out ClickHitbox hitbox))
            {
                IClickable clickable = hitbox.Clickable;
                if(_isShiftClick)
                {
                    clickable.OnShiftClick(this);
                }
                else
                {
                    clickable.OnClick(this);
                }
            }
            else
            {
                IssueMoveCommand(point);
            }
        }
    }

    private void DeselectUnits()
    {
        foreach (Unit u in SelectedUnits)
        {
            u.Deselect();
        }
        SelectedUnits.Clear();
    }

    public void SelectUnit(Unit unit)
    {
        DeselectUnits();
        SelectedUnits.Add(unit);
    }

    public void AddToSelection(Unit unit)
    {
        if (!SelectedUnits.Contains(unit))
        {
            SelectedUnits.Add(unit);
        }
    }


}
