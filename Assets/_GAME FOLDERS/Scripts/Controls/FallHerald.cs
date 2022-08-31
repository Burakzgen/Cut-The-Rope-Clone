using UnityEngine;
using System.Collections;

public class FallHerald : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TargetObjects"))
        {
            gameManager.FallTargetObject();
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Ball"))
        {
            gameManager.FallBall();
            collision.gameObject.SetActive(false);
        }
    }
}
