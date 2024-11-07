using UnityEngine; // Ensure these are at the very top
using System.Collections;

public class PlayAudioAndDespawn : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip to be played
    private AudioSource audioSource;
    private bool hasCollided = false;

    private void Start()
    {
        // Get or add an AudioSource component to the empty object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with this empty object is the player (character)
        if (collision.gameObject.CompareTag("Player") && !hasCollided) // Ensure collision happens with player and only once
        {
            hasCollided = true; // Set the flag to true to prevent multiple triggers
            
            // Start coroutine to handle the audio playback with delay
            StartCoroutine(PlayAudioWithDelay());
        }
    }

    private IEnumerator PlayAudioWithDelay()
    {
        // Wait for 5 seconds before playing the audio
        yield return new WaitForSeconds(0f);
        
        audioSource.Play(); // Play the audio clip
        
        // Disable the object's visibility and interaction while the audio plays
        HideObject();

        // Start coroutine to destroy object after audio finishes
        StartCoroutine(DestroyAfterAudio());
    }

    private void HideObject()
    {
        // Optionally disable the collider to prevent further interactions
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    // Coroutine to wait until the audio finishes playing, then destroy the object
    private IEnumerator DestroyAfterAudio()
    {
        // Wait for the audio clip to finish
        yield return new WaitForSeconds(audioClip.length);

        // Destroy the object
        Destroy(gameObject);
    }
}
