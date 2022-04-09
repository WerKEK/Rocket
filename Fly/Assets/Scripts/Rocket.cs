using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;
    [SerializeField] private float force = 1f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private AudioClip flySound;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private ParticleSystem flyParticle;
    [SerializeField] private ParticleSystem deadParticle;
    [SerializeField] private ParticleSystem finishParticle;

    bool isCollision = true;

    private enum State
    {
        Playing,
        Dead,
        NextLevel
    };

    State state = State.Playing;

    private void Start()
    {
        state = State.Playing;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (state == State.Playing)
        {
            Launch();
            Rotation();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Playing || !isCollision)
            return;

        switch (collision.gameObject.tag)
        {
            case "Finish":
                Finish();
                break;
            
            case "Friendly":
                break;

            default:
                Death();
                break;
        }
    }

    private void Finish()
    {
        state = State.NextLevel;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticle.Play();
        Invoke(nameof(NextLevel), 1f);
    }

    private void Death()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(deadSound);
        flyParticle.Stop();
        deadParticle.Play();
        Invoke(nameof(Lose), 2f); 
    }

    private void NextLevel()
    {
        int currentLvlIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLvlIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            currentLvlIndex = 0;
        }

        SceneManager.LoadScene(currentLvlIndex + 1);
    }

    private void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Launch()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(0, force * Time.deltaTime, 0);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(flySound);
                flyParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            flyParticle.Stop();
        }
    }

    private void Rotation()
    {
        rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
        }

        rb.freezeRotation = false;
    }
}
