using UnityEngine;

public class ClickMarker : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ReachedDestination();
        }
    }
}
