using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage = 10f; 

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            MainController player = hit.GetComponent<MainController>();
            
            if (player != null)
            {
                player.TakeDMG(damage); 
            }

            Destroy(gameObject);
        }
        else if (hit.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}