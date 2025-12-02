using UnityEngine;

public class Arm : MonoBehaviour
{
    private Hand handRef;

    public void SetHand(Hand hand)
    {
        handRef = hand;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage();
            player.TakeDamage();
            player.TakeDamage();
            handRef.Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
