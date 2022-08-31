using UnityEngine;

public class TargetObjectController : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            sound.Play();
        }
    }
}
