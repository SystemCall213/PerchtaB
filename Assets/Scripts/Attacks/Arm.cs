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
            print("Піймав");
            handRef.Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
