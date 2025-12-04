using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [Header("Drawing")]
    public float minPointDistance = 0.05f;
    public float allowedCheckpointDistance = 0.25f;

    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>();
    private Camera cam;

    [Header("Phase 1")]
    public PathBoundary[] boundariesPhase1;
    public CheckPoint[] checkpointsPhase1;

    [Header("Phase 2")]
    public PathBoundary[] boundariesPhase2;
    public CheckPoint[] checkpointsPhase2;

    private int phase = 1;
    private int nextCheckpointIndex = 0;

    [Header("Events")]
    public GameObject firstPhaseDone;
    public GameObject secondPhaseDone;
    public GameObject firstPhaseLine;
    public GameObject secondPhaseHeart;

    private bool done = false;

    private void Start()
    {
        cam = Camera.main;
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;

        StartPhase(1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ResetDrawingForCurrentPhase();

        if (Input.GetMouseButton(0))
        {
            DrawLine();

            CheckCheckpointProximity();
            CheckBoundaryViolation();   // NEW ✔✔✔
        }
    }

    // ===========================================================
    // ███    PHASE HANDLING
    // ===========================================================

    private void StartPhase(int newPhase)
    {
        phase = newPhase;
        nextCheckpointIndex = 0;

        if (phase == 1)
        {
            EnablePhase(checkpointsPhase1, boundariesPhase1);
            DisablePhase(checkpointsPhase2, boundariesPhase2);
        }
        else
        {
            if (!done)
            {
                secondPhaseHeart.SetActive(true);
                firstPhaseLine.SetActive(false);
                EnablePhase(checkpointsPhase2, boundariesPhase2);
                DisablePhase(checkpointsPhase1, boundariesPhase1);   
            }
        }
    }

    private void EnablePhase(CheckPoint[] cps, PathBoundary[] bnds)
    {
        foreach (var cp in cps)
        {
            cp.gameObject.SetActive(true);
            cp.Reset();            
        }

        foreach (var b in bnds)
        {
            b.gameObject.SetActive(true);
            b.active = false;            
        }

        if (bnds.Length > 0) bnds[0].active = true;
        if (cps.Length > 0) cps[0].isActive = true;
    }

    private void DisablePhase(CheckPoint[] cps, PathBoundary[] bnds)
    {
        foreach (var cp in cps)
            cp.gameObject.SetActive(false);

        foreach (var b in bnds)
            b.gameObject.SetActive(false);
    }

    // ===========================================================
    // ███    DRAWING
    // ===========================================================

    private void DrawLine()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;

        if (points.Count == 0 ||
            Vector3.Distance(points[^1], mousePos) > minPointDistance)
        {
            points.Add(mousePos);
            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());
        }
    }

    // ===========================================================
    // ███    CHECKPOINTS
    // ===========================================================

    private CheckPoint[] CurrentCheckpoints =>
        phase == 1 ? checkpointsPhase1 : checkpointsPhase2;

    private PathBoundary[] CurrentBoundaries =>
        phase == 1 ? boundariesPhase1 : boundariesPhase2;

    private void CheckCheckpointProximity()
    {
        var checkpoints = CurrentCheckpoints;

        if (nextCheckpointIndex >= checkpoints.Length) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        float dist = Vector3.Distance(
            mousePos,
            checkpoints[nextCheckpointIndex].transform.position
        );

        if (dist < allowedCheckpointDistance)
            HitCheckpoint(checkpoints[nextCheckpointIndex]);
    }

    public void HitCheckpoint(CheckPoint cp)
    {
        CheckPoint[] cps = CurrentCheckpoints;
        PathBoundary[] bnds = CurrentBoundaries;

        if (cps[nextCheckpointIndex] != cp)
        {
            return;
        }

        cp.MarkReached();

        nextCheckpointIndex++;

        if (nextCheckpointIndex - 1 >= 0 && nextCheckpointIndex - 1 < bnds.Length)
        {
            bnds[nextCheckpointIndex - 1].active = true;
            print(bnds[nextCheckpointIndex - 1]);
        }

        if (nextCheckpointIndex < cps.Length)
        {
            cps[nextCheckpointIndex].Activate();
        }
        else
        {
            PhaseComplete();
        }
    }

    private void PhaseComplete()
    {
        if (phase == 1)
        {
            firstPhaseDone.SetActive(true);
            StartPhase(2);
        }
        else
        {
            done = true;
            secondPhaseDone.SetActive(true);
            secondPhaseHeart.SetActive(false);
            DisablePhase(checkpointsPhase2, boundariesPhase2);
            StartCoroutine(FinishGame());
        }
    }

    // ===========================================================
    // ███    BOUNDARY VIOLATION
    // ===========================================================

    private void CheckBoundaryViolation()  // NEW ✔✔✔
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        bool shouldBeRepainted = true;

        foreach (var boundary in CurrentBoundaries)
        {
            if (!boundary.active) continue;

            // IMPORTANT:
            // PathBoundary *must* implement:
            // bool IsInside(Vector3 worldPosition)
            if (boundary.IsInside(mousePos))
            {
                shouldBeRepainted = false;
                return;
            }
        }

        if (shouldBeRepainted)
        {
            ResetDrawingForCurrentPhase();
        }
    }

    // ===========================================================
    // ███    RESET
    // ===========================================================

    public void ResetDrawingForCurrentPhase()
    {
        points.Clear();
        line.positionCount = 0;

        StartPhase(phase);
    }

    private IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(3f);
        SceneFader.Instance.FadeToScene("GoodEnding");
    }
}
