using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

    public float maxLaunchSpeed;
    public AudioClip windUpSound;
    public AudioClip launchSound;
    public PhysicsEngine launchedObjectPrefab;

    private AudioSource audioSource;
    private float launchSpeed;
    private float extraSpeedPerFrame;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windUpSound;     // So we know the length of the clip
        extraSpeedPerFrame = (maxLaunchSpeed * Time.fixedDeltaTime) / audioSource.clip.length;
    }

    private void OnMouseDown () {
        // Increase ball speed to max over a few seconds
        launchSpeed = 0;
        InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);
        audioSource.clip = windUpSound;
        audioSource.Play();
    }

    private void OnMouseUp() {
        CancelInvoke();     // cancel InvokeRepeating
        audioSource.Stop();
        audioSource.clip = launchSound;
        audioSource.Play();
        // Launch the ball
        PhysicsEngine launchedObjectInstance = Instantiate(launchedObjectPrefab, transform.position, Quaternion.identity) as PhysicsEngine;
        launchedObjectInstance.transform.parent = GameObject.Find("LaunchedObjects").transform;
        Vector3 launchVelocity = new Vector3(1, 1, 0).normalized * launchSpeed;
        launchedObjectInstance.velocityVector = launchVelocity;
    }

    private void IncreaseLaunchSpeed() {
        if (launchSpeed <= maxLaunchSpeed) {
            launchSpeed += extraSpeedPerFrame;
        }
    }
}
