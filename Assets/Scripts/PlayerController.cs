using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 2f;
    public float laneChangeSpeed = 10f;

    Vector2 startTouch;
    Vector2 endTouch;
    float swipeThreshold = 50f;

    int currentLane = 1;

    public GameManager gameManager;
    


    void Update()
    {
        MoveForward();
        HandleInput();
        HandleSwipe();
        MoveToLane();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0) currentLane--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2) currentLane++;
    }

    void HandleSwipe()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            startTouch = touch.position;

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

    void MoveToLane()
    {
        Vector3 target = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, laneChangeSpeed * Time.deltaTime);
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
    }*/

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
        }
    }
}