using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healAmount = 20; 
    public float lifeTime = 10f; 

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Mainch player = collision.GetComponent<Witch>();
            if (player != null)
            {
                player.AddHealth(healAmount);
                Destroy(gameObject); 
            }
        }
    }
}
