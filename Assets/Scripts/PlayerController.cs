using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement parameters
    private float forwardSpeed = 10f;
    private float laneDistance = 2f;
    private float laneChangeSpeed = 10f;
    private float swipeThreshold = 50f;
    private int currentLane = 1;

    // Scoring and speed progression
    private int score;
    private int nextMilestone = 50;

    private int coins;

    // Touch input variables
    private Vector2 startTouch;
    private Vector2 endTouch;

    // Reference to GameManager for score and game over handling
    [SerializeField] GameManager gameManager;

    // Main update loop for player movement and input handling
    void Update()
    {
        MoveForward();
        HandleInput();
        HandleSwipe();
        MoveToLane();
    }

    // Move the player forward continuously
    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    // Handle keyboard input for lane changes
    void HandleInput()
    {
        // For testing in editor with keyboard input
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0) currentLane--;

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2) currentLane++;
    }

    // Handle touch input for lane changes
    void HandleSwipe()
    {
        // For mobile input
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);

        // Record the start position of the touch
        if (touch.phase == TouchPhase.Began) startTouch = touch.position;

        // Record the end position of the touch and determine if it's a swipe
        if (touch.phase == TouchPhase.Ended)
        {
            endTouch = touch.position;
            float deltaX = endTouch.x - startTouch.x;
            if (Mathf.Abs(deltaX) > swipeThreshold)
            {
                if (deltaX > 0 && currentLane < 2) currentLane++;
                if (deltaX < 0 && currentLane > 0) currentLane--;
            }
        }
    }

    // Smoothly move the player to the target lane position
    void MoveToLane()
    {
        Vector3 target = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, laneChangeSpeed * Time.deltaTime);
    }

    // To detect collision with obstacle
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with obstacle");
            gameManager.GameOver();
        }
    }

    // To detect near miss
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Near Miss!!!");
            gameManager.Score();
            GetScore();
            while (score >= nextMilestone)
            {
                SpeedProgression();
                nextMilestone += 50; // Set the next milestone for speed increase
            }
        }

        if(other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collected an item!");
            gameManager.Coins();
            GetCoins();
            Destroy(other.gameObject); // Remove the collectible from the scene
        }
    }

    // Get the current score from GameManager and log it
    private void GetScore()
    {
        score = gameManager.GetScore();
        Debug.Log("Current Score: " + score);
    }

    // Increase the player's forward speed when a milestone is reached
    private void SpeedProgression()
    {
        forwardSpeed += 5f;
        Debug.Log("Speed Increased! Current Speed: " + forwardSpeed);
    }

    private void GetCoins()
    {
        coins = gameManager.GetCoins();
        Debug.Log("Current Coins: " + coins);
    }
}