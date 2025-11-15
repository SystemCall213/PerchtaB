using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private Transform position;

    public HPBar hPBar;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        position = GetComponent<Transform>();
    }

    public Transform GetTransform()
    {
        return position;
    }

    public PlayerMovement GetPlayerMovement()
    {
        return movement;
    }
    
    public void TakeDamage()
    {
        hPBar.takeDmg(1);
    }
}
