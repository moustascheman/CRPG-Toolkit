using System;
using UnityEngine;
using UnityEngine.AI;


namespace CRPGToolkit.Core
{
    /// <summary>
    /// Used for player controllable units and their movement. 
    /// </summary>
    public class Unit : MonoBehaviour , IClickable, ISelectable
    {

        [SerializeField]
        private GameObject UnitMarker;

        [SerializeField]
        private GameObject SelectionMarker;


        private NavMeshAgent _nAgent;


        public void Start()
        {
            _nAgent = GetComponent<NavMeshAgent>();
        }


        public void MoveToPoint(Vector3 point)
        {
           /*RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            var pos = hit.point;
            
            _nAgent.SetDestination(pos);
            





            //Debug.Log(nAgent.remainingDistance);
        }*/
            _nAgent.SetDestination(point);
        }


        public void Select()
        {
            UnitMarker.SetActive(false);
            SelectionMarker.SetActive(true);
        }

        public void Deselect()
        {
            UnitMarker.SetActive(true);
            SelectionMarker.SetActive(false);
        }

        public void OnClick(PartyController party)
        {
            party.SelectUnit(this);
            Select();
        }

        public void OnShiftClick(PartyController party)
        {
            party.AddToSelection(this);
            Select();
        }

        public void OnHoverStart()
        {
            throw new NotImplementedException();
        }

        public void OnHoverEnd()
        {
            throw new NotImplementedException();
        }

        public void OnDragSelect(PartyController party)
        {
            
        }
    }
}