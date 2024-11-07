using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class CheckpointTrigger : MonoBehaviour
{
    public PlayableDirector timelineDirector;  // Reference to the PlayableDirector that controls the Timeline
    public GameObject canvasElement;           // Reference to the Canvas or any UI element
    public float canvasDisplayTime = 5f;       // Time to show the canvas before hiding

    // This method is called when the player enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the checkpoint is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Play the Timeline cutscene
            timelineDirector.Play();

            // Subscribe to the timeline's stopped event (fired when the timeline finishes)
            timelineDirector.stopped += OnTimelineFinished;
        }
    }

    // Method called when the Timeline finishes
    private void OnTimelineFinished(PlayableDirector director)
    {
        // Activate the Canvas or UI element
        canvasElement.SetActive(true);

        // Start coroutine to hide the canvas after 5 seconds
        StartCoroutine(HideCanvasAfterDelay(canvasDisplayTime));

        // Unsubscribe to avoid multiple calls if the Timeline restarts
        timelineDirector.stopped -= OnTimelineFinished;
    }

    // Coroutine to hide the Canvas after the specified delay
    private IEnumerator HideCanvasAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        // Deactivate the Canvas or UI element
        canvasElement.SetActive(false);
    }
}
