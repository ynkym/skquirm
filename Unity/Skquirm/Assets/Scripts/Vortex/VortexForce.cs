using UnityEngine;
using System.Collections;
using System;

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
    private Mesh planeMesh;
    public GameObject testObject;
    [Range(-10.0f, 10.0f)]
    public float adjustTimeScale = 0.0f;

 

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        waterControll = waterPlane.GetComponent<WaterControll>();
        mat = waterPlane.GetComponent<Renderer>().sharedMaterial;
        heightmap = mat.GetTexture("_HeightMap") as Texture2D;
        originalWaterHorizon = waterHorizon;
        planeMesh = waterPlane.GetComponent<MeshFilter>().mesh;
    }

    bool isInArray(Vector3[] array, Vector3 target)
    {
        bool result = false;
        foreach(Vector3 item in array)
        {
            if (item == target)
            {
                result = true;
            }
        }
        return result;
    }

    bool isInTriangle(Vector2 p, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        // Using barycentric coordinate
        float denominator = ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));
        float a = ((p2.y - p3.y) * (p.x - p3.x) + (p3.x - p2.x) * (p.y - p3.y)) / denominator;
        float b = ((p3.y - p1.y) * (p.x - p3.x) + (p1.x - p3.x) * (p.y - p3.y)) / denominator;
        float c = 1 - a - b;
        return 0 <= a && a <= 1 && 0 <= b && b <= 1 && 0 <= c && c <= 1;
    }

  // Update is called once per frame
  void Update () {

        // The scale of width of the wave
        Vector4 waveHeightScale4 = waterControll.getWaveHeightScale4();
        //Vector4 heightOffset = waterControll.getHeightOffsetClamped();
        Vector4 heightOffset;
        Vector4 waveHeightSpeed = mat.GetVector("WaveHeightSpeed");
        float heightScale = mat.GetFloat("_WaveHeight");
        double t = Time.timeSinceLevelLoad / 20.0;
        //Vector3[] closest3Vectices = new Vector3[3];
        //float[] closest3VecticesDist = new float[3];
        //float[] closest3VecticesHeight = new float[3];

        //for (int i = 0; i < 3; i++)
        //{
        //    float mindistsqr = Mathf.Infinity;
        //    Vector3 nearestvertex = Vector3.zero;
        //    foreach (Vector3 vertex in planeMesh.vertices)
        //    {

        //        Vector3 diff = transform.position - waterPlane.transform.TransformPoint(vertex);
        //        float distsqr = diff.sqrMagnitude;

        //        if (distsqr < mindistsqr && !isInArray(closest3Vectices, vertex))
        //        {

        //            mindistsqr = distsqr;
        //            nearestvertex = vertex;
        //        }
        //    }
        //    Vector3 tempdiff = transform.position - waterPlane.transform.TransformPoint(nearestvertex);
        //    closest3VecticesDist[i] = new Vector2(tempdiff.x, tempdiff.z).magnitude;
        //    closest3Vectices[i] = nearestvertex;

        //    float adjustTimeScale = 0.0f;

        //    Vector3 nearestvertexWorld = waterPlane.transform.TransformPoint(nearestvertex);
        //    float tempx = nearestvertexWorld.x * waveHeightScale4.x + (heightOffset.x - waveHeightSpeed.x * Time.deltaTime * adjustTimeScale);
        //    float tempy = nearestvertexWorld.z * waveHeightScale4.y + (heightOffset.y - waveHeightSpeed.y * Time.deltaTime * adjustTimeScale);
        //    Color height1Col = heightmap.GetPixelBilinear(tempx, Mathf.Abs(1.0f-tempy));
        //    tempx = nearestvertexWorld.x * waveHeightScale4.z + (heightOffset.z - waveHeightSpeed.z * Time.deltaTime * adjustTimeScale);
        //    tempy = nearestvertexWorld.z * waveHeightScale4.w + (heightOffset.w - waveHeightSpeed.w * Time.deltaTime * adjustTimeScale);
        //    Color height2Col = heightmap.GetPixelBilinear(tempx,  Mathf.Abs(1.0f-tempy));
        //    float height1 = (height1Col.r + height1Col.g + height1Col.b) / 3.0f;
        //    float height2 = (height2Col.r + height2Col.g + height2Col.b) / 3.0f;
        //    float height = (height1 + height2) * 0.5f;

        //    closest3VecticesHeight[i] = height;
        //}

        Vector2[] triangleVectices = new Vector2[3];
        float[] triangleVecticesDist = new float[3];
        float[] triangleVecticesHeight = new float[3];
        int[] waterTriangles = planeMesh.triangles;
        Vector3[] waterVertices = planeMesh.vertices;
        int index = 0;
        bool triangleFound = false;
        Vector2 p = new Vector2(transform.position.x, transform.position.z);

        while (index < waterTriangles.Length && !triangleFound)
        {
            Vector3 tempP1 = waterPlane.transform.TransformPoint(waterVertices[waterTriangles[index]]);
            Vector3 tempP2 = waterPlane.transform.TransformPoint(waterVertices[waterTriangles[index + 1]]);
            Vector3 tempP3 = waterPlane.transform.TransformPoint(waterVertices[waterTriangles[index + 2]]);
            Vector2 p1 = new Vector2(tempP1.x, tempP1.z);
            Vector2 p2 = new Vector2(tempP2.x, tempP2.z);
            Vector2 p3 = new Vector2(tempP3.x, tempP3.z);

            //isInTriangle(new Vector2(1, 1), new Vector2(-1, -1), new Vector2(2, -2), new Vector2(1, 3));
            
            if (isInTriangle(p, p1, p2, p3))
            {
                triangleVectices[0] = p1;
                triangleVectices[1] = p2;
                triangleVectices[2] = p3;
                triangleVecticesDist[0] = (p1 - p).magnitude;
                triangleVecticesDist[1] = (p2 - p).magnitude;
                triangleVecticesDist[2] = (p3 - p).magnitude;
                for (int j = 0; j < 3; j++)
                {

                    heightOffset = new Vector4(
                        (float)Math.IEEERemainder(waveHeightSpeed.x * waveHeightScale4.x * t, 1.0),
                        (float)Math.IEEERemainder(waveHeightSpeed.y * waveHeightScale4.y * t, 1.0),
                        (float)Math.IEEERemainder(waveHeightSpeed.z * waveHeightScale4.z * t, 1.0),
                        (float)Math.IEEERemainder(waveHeightSpeed.w * waveHeightScale4.w * t, 1.0)
                        );

                   

                    float tempx = triangleVectices[j].x * waveHeightScale4.x + (heightOffset.x - waveHeightSpeed.x * Time.deltaTime * adjustTimeScale);
                    float tempy = triangleVectices[j].y * waveHeightScale4.y + (heightOffset.y - waveHeightSpeed.y * Time.deltaTime * adjustTimeScale);
                    Color height1Col = heightmap.GetPixelBilinear(1.0f-tempx, 1.0f-tempy);
                    tempx = triangleVectices[j].x * waveHeightScale4.z + (heightOffset.z - waveHeightSpeed.z * Time.deltaTime * adjustTimeScale);
                    tempy = triangleVectices[j].y * waveHeightScale4.w + (heightOffset.w - waveHeightSpeed.w * Time.deltaTime * adjustTimeScale);
                    Color height2Col = heightmap.GetPixelBilinear(1.0f-tempx, 1.0f-tempy);
                    float height1 = (height1Col.r + height1Col.g + height1Col.b) / 3.0f;
                    float height2 = (height2Col.r + height2Col.g + height2Col.b) / 3.0f;
                    float height = (height1 + height2) * 0.5f;

                    triangleVecticesHeight[j] = height * heightScale;

                    if (pullStrength > 0 && whirlStrength >0)
                    {
                        if (triangleVectices[j] == Vector2.zero)
                        {
                            float dheight = mat.GetFloat("decreaseHeight");
                            triangleVecticesHeight[j] = height - dheight;
                        }
                    }

                    
                }
                triangleFound = true;
            }
            index += 3;
        }


        // interpolate the position of the player in between the 3 vectices
        // the 3 vertices form a triangle with vertex a, b and c:
        //  1. interpolate between b and c and get d
        //  2. interpolate between a and d to get the correct height

        for (int i = 0; i < triangleVectices.Length; i++)
        {
            Instantiate(testObject, new Vector3(triangleVectices[i].x, 6, triangleVectices[i].y), Quaternion.identity);

        }

        // calculate angle in radian
        
        Vector2 vectorCP = p - triangleVectices[2];
        Vector2 vectorCB = triangleVectices[1] - triangleVectices[2];
        float angleACB = Vector2.Angle(vectorCP, vectorCB) *( Mathf.PI / 180f);
        float tCB = (triangleVecticesDist[2] * Mathf.Cos(angleACB)) / vectorCB.magnitude;

        // interpolate between b and c
        float heightCB = Mathf.Lerp(triangleVecticesHeight[2], triangleVecticesHeight[1], tCB);
        Vector2 vertexD = Vector2.Lerp(triangleVectices[2], triangleVectices[1], tCB);

        // interpolate between a and d
        Vector2 vectorAD = vertexD - triangleVectices[0];
        float tAD = triangleVecticesDist[0] / vectorAD.magnitude;
        float estHeight = Mathf.Lerp(triangleVecticesHeight[0], heightCB, tAD);

        // The scale of height of the wave

        float thresh;
        if (waterHorizon - (originalWaterHorizon + estHeight) >= 0)
        {
            thresh = 0.2f;
        } else
        {
            thresh = -0.2f;
        }
        waterHorizon = originalWaterHorizon + estHeight + thresh;
        //transform.position = new Vector3(transform.position.x, originalWaterHorizon + estHeight+thresh, transform.position.z);
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
