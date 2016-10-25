using UnityEngine;
using System.Collections;

public class UniversalGravitation: MonoBehaviour {

    private PhysicsEngine[] physicsEngineArray;

    private const float bigG = 6.673e-11f; // N(m^2/kg^2) [m^3 kg^-1 s^-2]

    private void Start() {
        physicsEngineArray = FindObjectsOfType<PhysicsEngine>();
    }

    // called about every 20ms
    void FixedUpdate() {
        CalculateGravity();
    }

    private void CalculateGravity() {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray) {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray) {
                if (physicsEngineA != physicsEngineB && physicsEngineA != this) {
                    //Debug.Log("Calculating gravitational force exerted on " + physicsEngineA.name + " due to the gravity of " + physicsEngineB.name);
                    Vector3 offset = (physicsEngineA.transform.position - physicsEngineB.transform.position);
                    float radiusSquared = Mathf.Pow(offset.magnitude, 2f);
                    float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / radiusSquared;
                    Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;
                    physicsEngineA.AddForce(-gravityFeltVector);
                }
            }
        }
    }
}
