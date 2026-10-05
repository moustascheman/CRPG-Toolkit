using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TravelLine : MonoBehaviour
{

    [SerializeField]
    private GameObject origin;

    [SerializeField]
    private NavMeshAgent agent;


    [SerializeField]
    private LineRenderer travelLine;



    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
            DrawPath();
        }
    }


    private void DrawPath()
    {
        NavMeshPath path = agent.path;
        travelLine.positionCount = path.corners.Length;

        travelLine.SetPositions(path.corners);
        travelLine.SetPosition(0, origin.transform.position);
    }


}
