using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTileManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform target;
    public float initialVelocity = 10f;
    public int resolution = 10;

    private void Start()
    {
        lineRenderer.positionCount = resolution + 1;
    }

    private void Update()
    {
        DrawTrajectory();
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[resolution + 1];
        float timeStep = Time.fixedDeltaTime / resolution;
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = (target.position - transform.position).normalized * initialVelocity;

        for (int i = 0; i <= resolution; i++)
        {
            positions[i] = currentPosition;
            currentPosition += currentVelocity * timeStep;
            currentVelocity += Physics.gravity * timeStep;
        }

        lineRenderer.SetPositions(positions);
    }
}
