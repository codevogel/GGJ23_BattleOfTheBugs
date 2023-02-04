using UnityEngine;

class Obstacle : MonoBehaviour
{
    public float size;

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(transform.position, size);
    }

    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = size;
    }
}