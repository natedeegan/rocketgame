
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource auidoSource;

    

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {

        auidoSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }
    
    void RespondToDebugKeys()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) {return;}
      


        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
  
    }

    void StartSuccessSequence()
    {   
        isTransitioning = true;
        auidoSource.Stop();
        auidoSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
       isTransitioning = true;
       auidoSource.Stop();
       auidoSource.PlayOneShot(crash);
       crashParticles.Play();
       GetComponent<Movement>().enabled = false;
       Invoke ("ReloadLevel",levelLoadDelay);
    }
    void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {

            nextSceneIndex = 0;
        }
        SceneManager.LoadScene (nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
