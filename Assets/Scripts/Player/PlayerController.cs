using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;
using CRPGToolkit.Core;

public class PlayerController : MonoBehaviour
{



    [SerializeField]
    private NavMeshAgent nAgent;

    [SerializeField]
    private GameObject destinationMarker;


    [SerializeField]
    private LineRenderer travelLine;

    
    private PlayerActions _actions;

    private InputManager mMan;

    void Awake()
    {
        /*_actions = new PlayerActions();
        _actions.PlayerIso.MoveCommand.Enable();
        _actions.PlayerIso.MoveCommand.started += OnMove;
        */
        mMan = InputManager.mouseManager;
        if(mMan == null)
        {
            Debug.LogError("Mouse Manager is null?");
        }
        mMan.GameplayMouseClicked += OnMove;
    }



    public void OnMove(object sender, EventArgs e)
    {
        Debug.Log("HIT");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            var pos = hit.point;
            //Debug.Log("OLD: " + pos.ToString());
            travelLine.gameObject.SetActive(true);
            nAgent.SetDestination(pos);
            var path = nAgent.path;
            destinationMarker.transform.position = pos;
            destinationMarker.SetActive(true);





            //Debug.Log(nAgent.remainingDistance);
        }
    }


    private void DrawPath()
    {
        var path = nAgent.path;
        travelLine.positionCount = path.corners.Length;

        travelLine.SetPosition(0, transform.position);
        if (path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < path.corners.Length; i++)
        {
            Vector3 pos = new Vector3(path.corners[i].x, path.corners[i].y, path.corners[i].z);
            travelLine.SetPosition(i, pos);
        }


    }

    public void ReachedDestination()
    {
        travelLine.gameObject.SetActive(false);
        Debug.Log("REACHED");
        destinationMarker.SetActive(false);

    }

}
