using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public struct SelectionBoxResults
{
}



public class SelectionBoxController : MonoBehaviour
{


    [SerializeField]
    private UIDocument _uiDocument;

    private VisualElement _selectionBox;


    private bool isEnabled = false;


    //Drag select variables
    private bool _hasTimerPassed = false;
    private bool _hasCursorMoved = false;

    [SerializeField]
    private float _dragSelectTime;

    [SerializeField]
    private float _dragMinDistance;

    private Vector2 _startingMousePosition = Vector2.zero;

    [SerializeField]
    private LayerMask selectableLayer;


    public void Awake()
    {
        _selectionBox = _uiDocument.rootVisualElement.Q("SelectionBox");
    }



    public void Update()
    {
        if (isEnabled)
        {
            if (_hasTimerPassed)
            {
                if (_hasCursorMoved)
                {
                    Vector2 currentPosition = Input.mousePosition;
                    float maxX = Mathf.Max(_startingMousePosition.x, currentPosition.x);
                    float minX = Mathf.Min(_startingMousePosition.x, currentPosition.x);
                    float maxY = Mathf.Max(Screen.height - _startingMousePosition.y, Screen.height - currentPosition.y);
                    float minY = Mathf.Min(Screen.height - _startingMousePosition.y, Screen.height - currentPosition.y);

                    _selectionBox.style.top = minY;
                    _selectionBox.style.left = minX;
                    _selectionBox.style.width = maxX - minX;
                    _selectionBox.style.height = maxY - minY;


                }
                else
                {
                    float dist = Vector2.Distance(_startingMousePosition, Input.mousePosition);
                    if (dist >= _dragMinDistance)
                    {
                        StartDragSelection();
                    }
                }
            }

        }
    }



    private void StartDragSelection()
    {
        _hasCursorMoved = true;
        // DragNotificationEvent.Invoke(this, null);

        Vector2 currentPosition = Input.mousePosition;
        float maxX = Mathf.Max(_startingMousePosition.x, currentPosition.x);
        float minX = Mathf.Min(_startingMousePosition.x, currentPosition.x);
        float maxY = Mathf.Max(Screen.height - _startingMousePosition.y, Screen.height - currentPosition.y);
        float minY = Mathf.Min(Screen.height - _startingMousePosition.y, Screen.height - currentPosition.y);

        _selectionBox.style.top = minY;
        _selectionBox.style.left = minX;
        _selectionBox.style.width = maxX - minX;
        _selectionBox.style.height = maxY - minY;



        _selectionBox.style.visibility = Visibility.Visible;

    }

    public void GetSelection()
    {

    }





    public void StartMouseClick(Vector2 mousePosition)
    {
        isEnabled = true;
        _startingMousePosition = mousePosition;
        _holdTimerRoutine = HoldTimer();
        StartCoroutine(_holdTimerRoutine);

    }

    public RaycastHit[] EndMouseClick()
    {
        StopCoroutine(_holdTimerRoutine);
        RaycastHit[] hits = null;
        if(_hasTimerPassed && _hasCursorMoved)
        {
            hits = GetSelectablesInBox();
        }
        isEnabled = false;
        _hasTimerPassed = false;
        _hasCursorMoved = false;
        _selectionBox.style.visibility = Visibility.Hidden;
        return hits;
    }


    private RaycastHit[] GetSelectablesInBox()
    {
        RaycastHit[] hits;
        float x1 = _selectionBox.resolvedStyle.left;
        float y1 = Screen.height - (_selectionBox.resolvedStyle.top + _selectionBox.resolvedStyle.height);
        float x2 = x1 + _selectionBox.resolvedStyle.width;
        float y2 = Screen.height - _selectionBox.resolvedStyle.top;

        Vector2 minScreenPoint = new Vector2(x1, y1);
        Vector2 maxScreenPoint = new Vector2(x2, y2);

        // Create a middle center viewport target or midpoint coordinate vector
        Vector2 screenCenter = (minScreenPoint + maxScreenPoint) / 2f;

        // Convert screen dimensions into world size measurements safely for Orthographic
        if (Camera.main.orthographic)
        {
            Vector3 worldCenter = Camera.main.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, Camera.main.nearClipPlane));

            float screenWidthUnits = maxScreenPoint.x - minScreenPoint.x;
            float screenHeightUnits = maxScreenPoint.y - minScreenPoint.y;

            // Ratio of Orthographic camera size bounds translation
            float pixelsToWorld = Camera.main.orthographicSize * 2f / Screen.height;
            Vector3 boxHalfExtents = new Vector3(screenWidthUnits * pixelsToWorld / 2f, screenHeightUnits * pixelsToWorld / 2f, 50f);

            // Cast forward from the orthographic alignment plane along forward track Vector
            hits = Physics.BoxCastAll(
                worldCenter,
                boxHalfExtents,
                Camera.main.transform.forward,
                Camera.main.transform.rotation,
                Mathf.Infinity,
                selectableLayer
            );
            return hits;
        }
        return null;

    }



    private IEnumerator _holdTimerRoutine;

    private IEnumerator HoldTimer()
    {
        yield return new WaitForSecondsRealtime(_dragSelectTime);
        _hasTimerPassed = true;
    }


}
