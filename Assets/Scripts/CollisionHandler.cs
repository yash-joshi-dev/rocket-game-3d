using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        
        //cheat codes for debugging and testing
        if(Input.GetKey(KeyCode.L)) {
            NextLevel();
        }
        else if(Input.GetKey(KeyCode.C)) {
            //disable collisions
            collisionDisable = !collisionDisable;
        }

    }
    void OnCollisionEnter(Collision other) {
        
        if(isTransitioning || collisionDisable) return;

        switch(other.gameObject.tag) {
            case "Friendly":
                Debug.Log("touching friendly stuff");
                break;
            case "Finish": 
                isTransitioning = true;
                GetComponent<Movement>().enabled = false;
                audioSource.Stop();
                audioSource.PlayOneShot(successSound);
                successParticles.Play();
                Invoke("NextLevel", nextLevelDelay);
                break;
            default: 
                isTransitioning = true;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
                GetComponent<Movement>().enabled = false;
                crashParticles.Play();
                Invoke("ReloadLevel", nextLevelDelay);
                break;
        }
    }

    void NextLevel() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);

    }
    void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
