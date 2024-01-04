using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] AudioClip mainThrustSound;    
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles    ;
    [SerializeField] ParticleSystem leftThrustParticles;

    
    Rigidbody rb;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)) {
            if(!audioSource.isPlaying) audioSource.PlayOneShot(mainThrustSound); 
            if(!mainThrustParticles.isPlaying) mainThrustParticles.Play();
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }

        if(Input.GetKey(KeyCode.LeftArrow)) {
            rb.freezeRotation = true; //freezing rotation so that we can manually rotate
            if(!rightThrustParticles.isPlaying) rightThrustParticles.Play();
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            rb.freezeRotation = false; //unfreezing rotation so that the physics system can take over

        }
        else if(Input.GetKey(KeyCode.RightArrow)) {
            rb.freezeRotation = true;
            if(!leftThrustParticles.isPlaying) leftThrustParticles.Play();
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            rb.freezeRotation = false;

        }
        else {
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }    
    }

}
