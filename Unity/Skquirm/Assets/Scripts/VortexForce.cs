using UnityEngine;
using System.Collections;

public class VortexForce : MonoBehaviour {

  private Rigidbody rb;
  public float waterHorizon;
  public float buoyancyStrength;
  public float pullStrength;
  public float whirlStrength;
  private float DAMPER =  0.9f;


  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update () {

  }

  void FixedUpdate () {

    // TODO: Clean up the calculation

    // How much "volume of water" is being pushed away?
    // (Not very accurate, adjust using the buoyancyStrength variable.)
    float volume = Mathf.Min(1, Mathf.Max(waterHorizon - rb.transform.position.y, 0));
    // buoyancy force strength
    float buoyancy = buoyancyStrength * volume;

    // world position of the cube
    Vector3 wp = transform.TransformPoint(new Vector3(0,0,0));
    // get current velocity of the cube to damp the movement
    Vector3 velocity = rb.GetPointVelocity(wp);
    // compute damping force
    Vector3 localDampingForce = -velocity * DAMPER * rb.mass;
    // buoyancy force
    Vector3 force = localDampingForce + new Vector3(0, buoyancy, 0);
    // pull towards the center (position <0,0,0>).
    Vector3 pull = -1 * pullStrength * rb.transform.position;

    // Apply torque to make the object move in circular path
    Vector3 whirl = new Vector3(rb.transform.position.z, 0, -rb.transform.position.x);
    // To make strength calculation simpler, normalize first
    whirl.Normalize();
    // and multiply by whirlStrength
    whirl = whirlStrength * whirl;


    //Debug.Log(force);

    // add all forces together and apply to the rb
    rb.AddForce(force + whirl + pull);
  }


}
