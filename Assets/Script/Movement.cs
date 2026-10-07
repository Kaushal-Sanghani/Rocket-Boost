using UnityEngine;

using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotate;
    [SerializeField] float thruststrength = 100f;
    [SerializeField] float rotationstrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotate.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
             StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thruststrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
        if (rotate.IsPressed())
        {
            float rotationInput = rotate.ReadValue<float>();
            if (rotationInput < 0)
            {
                Rotateright();

            }
            else if (rotationInput > 0)
            {
                Rotateleft();
            }
        }
        else
        {
            StopRotating();
        }

    }

    private void Rotateright()
    {
        ApplyRotation(rotationstrength);
        if (!rightThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Play();
        }
    }
    private void Rotateleft()
    {
        ApplyRotation(-rotationstrength);
        if (!leftThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Play();
        }
    }
    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void ApplyRotation(float RotateThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * RotateThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}