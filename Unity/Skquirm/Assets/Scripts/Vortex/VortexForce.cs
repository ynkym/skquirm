using UnityEngine;
using System.Collections;

public class VortexForce : MonoBehaviour {

  private Rigidbody rb;
  public float waterHorizon;
  public float buoyancyStrength;
  public float pullStrength;
  public float whirlStrength;
    public GameObject waterPlane;
  private float DAMPER =  0.9f;
    private object vector3;

    private float originalWaterHorizon;
    private WaterControll waterControll;
    private Material mat;
    private Texture2D heightmap;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        waterControll = waterPlane.GetComponent<WaterControll>();
        mat = waterPlane.GetComponent<Renderer>().sharedMaterial;
        heightmap = mat.GetTexture("_HeightMap") as Texture2D;
        originalWaterHorizon = waterHorizon;
    }

  // Update is called once per frame
  void Update () {
        //Mesh mesh = waterPlane.GetComponent<MeshFilter>().mesh;
        //float mindistsqr = Mathf.Infinity;
        //Vector3 nearestvertex = Vector3.zero;
        //Vector3 wp = transform.TransformPoint(new Vector3(0, 0, 0));


        //foreach (Vector3 vertex in mesh.vertices)
        //{
        //    Vector3 diff = wp - vertex;
        //    float distsqr = diff.sqrMagnitude;

        //    if (distsqr < mindistsqr)
        //    {
        //        mindistsqr = distsqr;
        //        nearestvertex = vertex;
        //    }
        //}
        //Debug.Log(waterPlane.transform.TransformPoint(nearestvertex));
        //waterHorizon = nearestvertex.y;

        // The scale of width of the wave
        Vector4 waveHeightScale4 = waterControll.getWaveHeightScale4();
        Vector4 heightOffset = waterControll.getHeightOffsetClamped();
        float tempx = transform.position.x * waveHeightScale4.x + heightOffset.x;
        float tempy = transform.position.z * waveHeightScale4.y + heightOffset.y;
        Color height1Col = heightmap.GetPixelBilinear(tempx, tempy);
        tempx = transform.position.x * waveHeightScale4.z + heightOffset.z;
        tempy = transform.position.z * waveHeightScale4.w + heightOffset.w;
        Color height2Col = heightmap.GetPixelBilinear(tempx, tempy);
        float height1 = (height1Col.r + height1Col.g + height1Col.b) / 3.0f;
        float height2 = (height2Col.r + height2Col.g + height2Col.b) / 3.0f;
        float height = (height1 + height2) * 0.5f;

        // The scale of height of the wave
        float heightScale = mat.GetFloat("_WaveHeight");
        waterHorizon = originalWaterHorizon + height * heightScale;
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
