using System.Collections;
using UnityEngine;

public class Splinter : MonoBehaviour
{
    public float speed = 5f;
    protected Transform target;
    private bool isMoving = false;

    public void Move(Transform end)
    {
        target = end;
        FaceTarget();
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving || target == null)
            return;

        // Move toward target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if reached
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            isMoving = false;
            Despawn();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
    }

    protected void Despawn()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Піймав");
        }
    }
}
