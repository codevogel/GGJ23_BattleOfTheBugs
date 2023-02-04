using UnityEngine;

class Obstacle : MonoBehaviour
{
    public float size;

    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = size;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("RootEnd"))
        {
            return;
        }
        collision.GetComponent<RootMotor>().Slowed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("RootEnd"))
        {
            return;
        }
        collision.GetComponent<RootMotor>().Slowed = false;
    }
}