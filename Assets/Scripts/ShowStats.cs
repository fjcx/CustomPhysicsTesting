using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShowStats : MonoBehaviour {

    private Rigidbody rigidBody;

	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        Debug.Log(rigidBody.inertiaTensor);
	}
}
