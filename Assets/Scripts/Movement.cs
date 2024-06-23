using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftthrusterParticles;
    [SerializeField] ParticleSystem rightthrusterParticles;


    Rigidbody rb;
    AudioSource auidoSource;


    
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       auidoSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
void ProcessThrust()

{
   if (Input.GetKey(KeyCode.Space))
   {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //could also do 0,1,0
        if(!auidoSource.isPlaying)
        {
        auidoSource.PlayOneShot(mainEngine);
        }
        if(!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
            
        }
   }
   else
   {

    auidoSource.Stop();
    mainEngineParticles.Stop();
   }
}
void ProcessRotation()
{
     if (Input.GetKey(KeyCode.A))
    
        {
            ApplyRotation(rotationThrust);
            if(!rightthrusterParticles.isPlaying)
            {
                rightthrusterParticles.Play();
            
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {

            ApplyRotation(-rotationThrust);
            if (!leftthrusterParticles.isPlaying)
            {
                leftthrusterParticles.Play(); 
            }
        }
        else
        {
            rightthrusterParticles.Stop();
            leftthrusterParticles.Stop();
        }
    }

    void ApplyRotation(float RotationThisFrame)
   
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * RotationThisFrame * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
    

