using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput() {

        if(Input.GetKey(KeyCode.UpArrow)) {
            //do something
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }


        if(Input.GetKey(KeyCode.LeftArrow)) {
            rb.freezeRotation = true; //freezing rotation so that we can manually rotate
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            rb.freezeRotation = false; //unfreezing rotation so that the physics system can take over

        }
        else if(Input.GetKey(KeyCode.RightArrow)) {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            rb.freezeRotation = false;

        }
    }
}
