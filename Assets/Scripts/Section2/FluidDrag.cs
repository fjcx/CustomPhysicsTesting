using UnityEngine;
using System.Collections;

public class FluidDrag : MonoBehaviour {

    [Range(1f, 2f)]
    public float velocityExponent;          // Add low velocities use power 1 (Stokes's drag eq), at higher velocities use power 2
    public float dragConstant;              // [??]

    private PhysicsEngine physicsEngine;

    private void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate() {
        Vector3 velocityVector = physicsEngine.velocityVector;
        float speed = velocityVector.magnitude;
        float dragSize = CalculateDrag(speed);
        Vector3 dragVector = dragSize * -velocityVector.normalized;
        physicsEngine.AddForce(dragVector);
    }
	
	private float CalculateDrag(float velocity) {
        return dragConstant * Mathf.Pow(velocity, velocityExponent);
    }
}
