using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;



public class ClickHitbox : MonoBehaviour
{

    [SerializeField]
    private GameObject Base;

    public IClickable Clickable {get; private set;}
    public ISelectable Selectable  {get; private set;}



    public void Start()
    {
        if(Base.TryGetComponent<IClickable>(out IClickable clickable)){
            Clickable = clickable;
        }
        else
        {
            Debug.LogError("ERROR: Base of ClickHitbox needs to be an IClickable");
        }
        Base.TryGetComponent<ISelectable>(out ISelectable selectable);
        Selectable = selectable;
    }


}
