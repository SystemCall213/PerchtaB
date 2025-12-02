using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public float minPointDistance = 0.05f;     // how often to add points
    public float maxDistanceFromPath = 1f;     // how far player can wander off allowed path
    public PathBoundary[] boundaries;        // center line reference (curve or straight)
    public float allowedPathRadius = 1.0f;     // width of acceptable path corridor

    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>();
    private Camera cam;

    private int nextCheckpointIndex = 0;
    public CheckPoint[] checkpoints;            // assign in inspector

    private void Start()
    {
        cam = Camera.main;
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        boundaries[0].active = true;
        checkpoints[0].isActive = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetDrawing();
        }

        if (Input.GetMouseButton(0))
        {
            DrawLine();
            CheckCheckpointProximity();
        }
    }

    private void DrawLine()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;

        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePos) > minPointDistance)
        {
            points.Add(mousePos);
            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());
        }
    }

    // Called by checkpoints
    public void HitCheckpoint(CheckPoint cp)
    {
        // Is this the correct next checkpoint?
        if (checkpoints[nextCheckpointIndex] == cp)
        {
            // Mark the checkpoint as reached (turn green, stop rotation)
            cp.MarkReached();

            // When a checkpoint is reached, we disable the boundary that led to it
            int previousBoundaryIndex = nextCheckpointIndex - 1;
            if (previousBoundaryIndex >= 0 && previousBoundaryIndex < boundaries.Length)
                boundaries[previousBoundaryIndex].active = false;

            nextCheckpointIndex++;

            // Now activate the boundary that leads to the NEXT checkpoint
            // (if there is one)
            if (nextCheckpointIndex - 1 >= 0 && nextCheckpointIndex - 1 < boundaries.Length)
                boundaries[nextCheckpointIndex - 1].active = true;

            // Activate next checkpoint rotation
            if (nextCheckpointIndex < checkpoints.Length)
                checkpoints[nextCheckpointIndex].Activate();
        }
        else
        {
            ResetDrawing();
        }
    }

    private void CheckCheckpointProximity()
    {
        if (nextCheckpointIndex >= checkpoints.Length)
            return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        CheckPoint target = checkpoints[nextCheckpointIndex];

        float distance = Vector3.Distance(mousePos, target.transform.position);

        if (distance < 0.25f)
        {
            HitCheckpoint(target);   // accept checkpoint
        }
    }

    public void ResetDrawing()
    {
        points.Clear();
        line.positionCount = 0;

        // Reset checkpoints
        nextCheckpointIndex = 0;
        foreach (var cp in checkpoints)
            cp.Reset();

        // Deactivate all boundaries
        foreach (var b in boundaries)
            b.active = false;

        // Activate the first boundary
        boundaries[0].active = true;
        checkpoints[0].isActive = true;

        Debug.Log("Drawing reset!");
    }

}
