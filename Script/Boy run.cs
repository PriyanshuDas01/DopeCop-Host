using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class NPCController : MonoBehaviour
{
    public PlayableDirector timeline;     // Reference to the Timeline
    public NavMeshAgent navAgent;         // Reference to the NavMeshAgent component
    public Transform checkpoint;          // Target checkpoint where NPC should run to

    private void Start()
    {
        // Ensure the timeline and NavMeshAgent are assigned
        if (timeline == null)
        {
            timeline = GetComponent<PlayableDirector>();
        }

        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        // Subscribe to the timeline's "stopped" event
        timeline.stopped += OnTimelineFinished;
    }

    // Method triggered when the Timeline finishes
    void OnTimelineFinished(PlayableDirector pd)
    {
        if (pd == timeline)
        {
            // Set NPC to run towards the checkpoint
            navAgent.SetDestination(checkpoint.position);
            navAgent.isStopped = false;  // Resume movement if stopped

            Debug.Log("Timeline finished. NPC is now running to the checkpoint.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (timeline != null)
        {
            timeline.stopped -= OnTimelineFinished;
        }
    }
}
