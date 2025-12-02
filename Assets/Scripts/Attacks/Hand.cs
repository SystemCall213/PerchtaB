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
    public float timeBetweenExtending = 0.5f;
    public Arm armPrefab;


    [Header("Settings")]
    public float turnDegreesPerStep = 30f;
    public float teleportDistance = 3f;

    private List<Arm> arms;
    private List<Vector3> handPositions = new List<Vector3>(); 
    private List<Quaternion> handRotations = new List<Quaternion>();

    private void Awake()
    {
        arms = new List<Arm>();
        numOfTries = Random.Range(minNumOfTries, maxNumOfTries);
    }

    private void Start()
    {
        handPositions.Add(transform.position);
        handRotations.Add(transform.rotation);
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
                delta = Mathf.Clamp(delta, -75f, 75f);

                // --- Step 2: Rotate gradually using transform.Rotate ---
                float duration = 0.35f;
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
                handPositions.Add(endPos);
                handRotations.Add(transform.rotation);

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

        yield return StartCoroutine(ReturnBackwards());

        Kill();
    }

    private IEnumerator ReturnBackwards()
    {
        // Move from last stored position back to first
        for (int i = handPositions.Count - 2; i >= 0; i--)    
        {
            // Teleport Hand to previous position and rotation
            transform.position = handPositions[i];
            transform.rotation = handRotations[i];

            // Destroy the last Arm associated with this segment
            if (arms.Count > 0)
            {
                Arm lastArm = arms[arms.Count - 1];
                arms.RemoveAt(arms.Count - 1);
                lastArm.Kill();
            }

            // Optional small delay to make return step visible
            yield return new WaitForSeconds(0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage();
            player.TakeDamage();
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
