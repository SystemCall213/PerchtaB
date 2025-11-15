using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("References")]
    protected Transform target;
    private int numOfTries;
    public int minNumOfTries = 7;
    public int maxNumOfTries = 10;
    public float timeBetweenExtending = 1.5f;
    public Arm armPrefab;


    [Header("Settings")]
    public float turnDegreesPerStep = 30f;
    public float teleportDistance = 3f;
    public float interval = 1f;

    private List<Arm> arms;

    private void Awake()
    {
        arms = new List<Arm>();
        numOfTries = Random.Range(minNumOfTries, maxNumOfTries);
    }

    private void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    public void SetPlayer(Transform _playerPos)
    {
        target = _playerPos;
    }

    private IEnumerator FollowPlayer()
    {
        for (int i = 0; i < numOfTries; i++)
        {
            // rotate and move
            if (target != null)
            {
                // --- Step 1: Determine the desired Z rotation ---
                Vector3 toPlayer = target.position - transform.position;

                float desiredZ = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
                desiredZ -= 90f;

                float currentZ = transform.eulerAngles.z;

                // normalize delta to [-180, 180]
                float delta = Mathf.DeltaAngle(currentZ, desiredZ);

                // ✅ NEW: Limit rotation per step to ±45 degrees
                delta = Mathf.Clamp(delta, -45f, 45f);

                // --- Step 2: Rotate gradually using transform.Rotate ---
                float duration = 1f;
                float t = 0f;

                while (t < duration)
                {
                    t += Time.deltaTime;

                    float step = (delta / duration) * Time.deltaTime;

                    transform.Rotate(0f, 0f, step);

                    yield return null;
                }

                transform.rotation = Quaternion.Euler(0, 0, currentZ + delta);

                Vector3 startPos = transform.position;         // Starting point before movement
                Quaternion startRot = transform.rotation;      // Rotation before movement

                // --- Step 3: Move after rotation ---
                Vector3 endPos = startPos + transform.up * teleportDistance;
                transform.position = endPos;

                Vector3 midPos = (startPos + endPos) * 0.5f;   // midpoint
                Quaternion armRot = startRot;                  // same direction as the hand rotated

                Arm arm = Instantiate(armPrefab, midPos, armRot);
                arms.Add(arm);
                arm.SetHand(this);

                // --- Step 3: Move after rotation ---
                transform.position += transform.up * teleportDistance;
            }

            yield return new WaitForSeconds(timeBetweenExtending);
        }

        Kill();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage();
            Kill();
        }
    }
    
    public void Kill()
    {
        foreach (Arm arm in arms)
        {
            arm.Kill();
        }
        Destroy(gameObject);
    }
}
