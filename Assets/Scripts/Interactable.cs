using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    Transform player;

    void Update ()
    {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
            Debug.Log("INTERACT");
            Debug.Log("AAAAAAAAAAAA");
            }
    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
