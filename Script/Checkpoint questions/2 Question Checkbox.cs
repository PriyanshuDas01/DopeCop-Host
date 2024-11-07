using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerQuestionSystem1 : MonoBehaviour
{
    public Canvas questionCanvas;        // Reference to the Canvas with the question
    public TextMeshProUGUI questionText; // Reference to the TextMeshPro text for the question
    public GameObject correctOption;     // GameObject for the correct answer option
    public GameObject wrongOption;       // GameObject for the wrong answer option
    public TextMeshProUGUI feedbackText; // Reference to the TextMeshPro for showing feedback

    public Canvas scoreCanvas;           // Reference to the Canvas for displaying the score
    public TextMeshProUGUI scoreText;    // Reference to the TextMeshPro text to display the score

    private bool questionActive = false; // To check if the question is active

    // Called when the player enters the checkpoint trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a checkpoint
        if (other.CompareTag("Checkpoint1") && !questionActive)
        {
            questionActive = true;
            DisplayQuestion();  // Show the question when the player reaches the checkpoint
        }

        // Check for answer options if the question is active
        if (questionActive)
        {
            if (other.gameObject == correctOption)
            {
                HandleCorrectAnswer();
                DestroyElements(); // Destroy elements after the correct answer
            }
            else if (other.gameObject == wrongOption)
            {
                HandleWrongAnswer();
                DestroyElements(); // Destroy elements after the wrong answer
            }
        }
    }

    // Function to display the question and show answer options
    private void DisplayQuestion()
    {
        questionCanvas.gameObject.SetActive(true); // Enable the question canvas


        // Activate the correct and wrong answer choices
        correctOption.SetActive(true);  // Show the correct answer choice
        wrongOption.SetActive(true);    // Show the wrong answer choice
    }

    // Handle correct answer logic
    private void HandleCorrectAnswer()
    {
        GameManager.instance.IncreaseScore();  // Increase score using GameManager
        UpdateScore();  // Update the score display on the score canvas
        feedbackText.text = "Correct!";
        feedbackText.color = Color.green;

        Debug.Log("Correct answer given!"); // Debugging message

        // Show feedback temporarily
        StartCoroutine(ShowFeedback());
    }

    // Handle wrong answer logic
    private void HandleWrongAnswer()
    {
        feedbackText.text = "Wrong Answer!";
        feedbackText.color = Color.red;

        Debug.Log("Wrong answer given!"); 

        // Show feedback temporarily
        StartCoroutine(ShowFeedback());
    }

    private IEnumerator ShowFeedback()
    {
        feedbackText.gameObject.SetActive(true); // Ensure feedback text is visible
        yield return new WaitForSeconds(2f); // Show feedback for 2 seconds
        feedbackText.gameObject.SetActive(false); // Hide feedback text
    }

    // Update the score on the score canvas
    private void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.instance.GetPlayerScore().ToString();  // Update the score text
        scoreCanvas.gameObject.SetActive(true);  
    }

    // Destroy the options and canvas elements after answering
    private void DestroyElements()
    {
        Destroy(correctOption);  
        Destroy(wrongOption);   
        Destroy(questionCanvas.gameObject);  
        questionActive = false; 
    }
}
