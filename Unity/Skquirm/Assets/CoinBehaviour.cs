using UnityEngine;
using System.Collections;

public class CoinBehaviour : MonoBehaviour {

    [SerializeField] float rotationSpeed;
    [SerializeField] float min_height;
    [SerializeField] float y_velocity;
    float boyancy_factor = 1f;
    float horizontal_factor = 1f;
    Vector3 euler_vector = new Vector3(0f, 0f, 1f);


    Vector3 velocity_horizontal, velocity_y;
    Vector3 delta_space_horizontal;
    Vector3 height;
    Vector3 starting_point;

    [SerializeField] bool jumping_aside = true;
    bool trigger_rotation = false;
    bool trigger_animation = false;
    int max_blink = 15;
    int blink = 0;


    //Time settings
    float start_time;

	// Use this for initialization
	void Start () {

        //ThrowCoin(transform.position, new Vector3(3f, 2f, 1f));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vanish();
        }
    }

	void FixedUpdate () {
        if ( trigger_rotation ) transform.Rotate(euler_vector * rotationSpeed);

        if ( trigger_animation )
        {
            float sin = Mathf.Sin(y_velocity*(Time.time - start_time));

            if (sin >= -0.2f) {
                if(jumping_aside) delta_space_horizontal = horizontal_factor * velocity_horizontal * (Time.time - start_time);
                height = boyancy_factor * velocity_y * sin ;

                transform.position = starting_point + height + delta_space_horizontal;
            }
            else
            {
                boyancy_factor = 0.3f;
                horizontal_factor = 0f;
                if (!trigger_rotation)
                {
                    starting_point = transform.position;
                    trigger_rotation = true;
                    gameObject.GetComponent<Collider>().enabled = true;
                }
                
                start_time = Time.time;
            }
        }
	}

    public void StartCoinRotation() {
        trigger_rotation = true;
    }

    // ANIMATION FUNCTIONS

    public void ThrowCoin(Vector3 starting_point, Vector3 velocity, bool jump = true) {
        velocity_horizontal = ProjectVectorOnPlane(new Vector3(0f, 1f, 0f), velocity);
        velocity_y = Vector3.Project(velocity, new Vector3(0f, 1f, 0f));

        this.starting_point = starting_point;
        start_time = Time.time;

        trigger_animation = true;
        if (jump == true) trigger_rotation = false;
        jumping_aside = jump;
        horizontal_factor = 1f;
        boyancy_factor = 1f;
        blink = 0;
    }

    public void Vanish() {
            StartCoroutine(VanishingAnimation());
    }

    public IEnumerator VanishingAnimation() {
        if (blink < max_blink)
        {
            blink++;
            GetComponent<SkinnedMeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SkinnedMeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(VanishingAnimation());
        }
    }



    // MATH FUNCTIONS

    Vector3 ProjectVectorOnPlane(Vector3 planeNormal , Vector3 v )  {
        planeNormal.Normalize();
        float distance = -Vector3.Dot(planeNormal.normalized, v);
        return v + planeNormal* distance;
    }

    void OnTriggerEnter(Collider other) {
        gameObject.SetActive(false);
    }


    }
