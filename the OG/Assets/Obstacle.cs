using UnityEngine;

class Obstacle : MonoBehaviour
{
    public float size;
    private AudioSource audioSource;

    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = size;
        audioSource =  GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RootEnd")
        {
            audioSource.Play();
        }
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