using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

    public float fuelMass;                              // [kg]
    public float maxThrust;                             // kN [kg m s^-2]
    [Range (0, 1f)]
    public float thrustPercent;                         // [none] - a percentage
    public Vector3 thrustUnitVector = new Vector3();    // [none] - a unit vector

    private PhysicsEngine physicsEngine;
    private float currentThrust;                        // N (not kN)

    void Start() {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }

    void FixedUpdate() {
        float fuelThisUpdate = FuelThisUpdate();
        if (fuelMass > fuelThisUpdate) {
            fuelMass -= fuelThisUpdate;
            physicsEngine.mass -= fuelThisUpdate;
            ExertForce();
        } else {
            Debug.LogWarning("Out of rocket fuel");
        }
    }

    private float FuelThisUpdate() {
        float exhaustMassFlow;                      // [kg s-1]
        float effectiveExhaustVelocity;             // [m s-1]

        effectiveExhaustVelocity = 4462f;           //  [m s-1] liquid H O
        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime;    // [kg]
    }

    private void ExertForce() {
        currentThrust = thrustPercent * maxThrust * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust;    // N
        physicsEngine.AddForce(thrustVector);
    }

}
