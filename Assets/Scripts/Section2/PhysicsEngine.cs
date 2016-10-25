using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {
    public float mass;              // [kg]
    public Vector3 velocityVector;  // [m s^-1]
    public Vector3 netForceVector;  // N [kg m s^-2]
    
    private List<Vector3> forceVectorList = new List<Vector3>();

    // Use this for initialization
    void Start () {
        SetupThrustTrails();
    }

    // called about every 20ms
    void FixedUpdate() {
        RenderTrails();
        UpdatePosition();
    }

    public void AddForce(Vector3 forceVector) {
        forceVectorList.Add(forceVector);
        //Debug.Log("Adding force " + forceVector + " to " + gameObject.name);
    }

    private void UpdatePosition() {
        // Sum forces and clear the force list
        netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }
        forceVectorList = new List<Vector3>();

        // Update velocity
        Vector3 accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;

        // Update position due to net force
        transform.position += velocityVector * Time.deltaTime;
    }


    /// Code for drawing thrust trails
    public bool showTrails = true;
    private LineRenderer lineRenderer;
    private int numberOfForces;

    private void SetupThrustTrails() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    private void RenderTrails() {
        // From DrawForce.cs
        if (showTrails) {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        } else {
            lineRenderer.enabled = false;
        }
    }

}
