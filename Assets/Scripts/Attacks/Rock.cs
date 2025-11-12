using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 5f;
    protected Transform target;
    private bool isMoving = false;
    private float activeSeconds = 0.5f;
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void Move(Transform end)
    {
        target = end;
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving || target == null)
            return;

        // Move toward target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if reached
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            col.enabled = true;

            isMoving = false;
            Despawn();
        }
    }

    protected void Despawn()
    {
        StartCoroutine(Kill());
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(activeSeconds);
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
