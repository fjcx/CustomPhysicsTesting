using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PhysicsEngine))]
public class ApplyForce : MonoBehaviour {

    public Vector3 forceVector = new Vector3();

    private PhysicsEngine physicsEngine;

    void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    void FixedUpdate() {
        physicsEngine.AddForce(forceVector);
    }
}
