using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horinput;
    float vertinput;
    public float speed;
    public float rotationspeed;
    float flipSpeed = 500;

    Rigidbody rb;

    public static bool upsidedown = false;
    bool faceDown = false;

    float Ypos=0.21f;

    Vector3 spawnpos;
   // LayerMask layerMask;   
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnpos = transform.position;
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        horinput = Input.GetAxis("Horizontal");
        vertinput = Input.GetAxis("Vertical");


        rb.AddForce(transform.TransformDirection(Vector3.forward) * vertinput * speed * Time.deltaTime, ForceMode.Force);
        rb.angularVelocity = new Vector2(rb.velocity.x, horinput * rotationspeed * Time.deltaTime);       

        if (Input.GetKeyDown(KeyCode.Space))
        {           
           // ToggleUpsideDown();
            upsidedown = true;
        }        

        if (upsidedown)
        {
            transform.Rotate(Vector3.forward * flipSpeed * Time.deltaTime);
            if (!faceDown && transform.eulerAngles.z > 180)
            {
                Ypos = -2f;
                upsidedown = false;
                faceDown = true;
                Physics.gravity = new Vector3(0, 9.8f, 0);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 180);
                transform.position = new Vector3(transform.position.x, Ypos, transform.position.z);
                spawnpos = new Vector3(spawnpos.x, Ypos, spawnpos.z);
                
            }

            if (faceDown && 180-transform.eulerAngles.z > 0)
            {
                Ypos = 0.21f;
                upsidedown = false;
                faceDown = false;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0);
                transform.position = new Vector3(transform.position.x, Ypos, transform.position.z);
                spawnpos = new Vector3(spawnpos.x, Ypos, spawnpos.z);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FallDetect")
        {
            transform.position = spawnpos;
        }
    }
}
