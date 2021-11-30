using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
            Destroy(other.attachedRigidbody.gameObject);
        else
            Destroy(other.gameObject);
    }
}
