using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;


    [SerializeField]
    private float x;

    [SerializeField]
    private float y;

    [SerializeField]
    private float z;

    void LateUpdate()
    {
        transform.position = playerTransform.position + new Vector3(x, y, z);
    }
}
