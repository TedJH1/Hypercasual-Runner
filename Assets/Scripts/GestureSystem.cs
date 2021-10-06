using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSystem : MonoBehaviour
{
    private Touch touch;
    private Vector2 beginTouch, endTouch;
    private float timer;
    public bool jetpackEnabled;
    public bool isMoving, isGrounded;
    public GameObject player;
    public PlayerMovement playerMovement;
    public Animator animator;
    public bool movementEnabled;

    private void Start()
    {
        jetpackEnabled = false;
        isMoving = false;
        movementEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = false;
        if (Physics.Raycast(player.transform.position, -player.transform.up, out RaycastHit hit))
        {
            isGrounded = Mathf.Approximately(hit.distance, 1.05f);
        }
        animator.SetBool("Grounded", isGrounded);
        if (Input.touchCount > 0 && movementEnabled)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                // Record initial touch position and start timer
                case TouchPhase.Began:
                    timer = 0f;
                    beginTouch = touch.position;
                    break;

                // If touch held in same position for 0.5s, enable jetpack
                case TouchPhase.Stationary:
                    endTouch = touch.position;
                    timer += Time.deltaTime;
                    if (timer >= 0.1f && endTouch.x >= beginTouch.x - 10f && endTouch.x <= beginTouch.x + 10f)
                    {
                        if (!jetpackEnabled && playerMovement.rocketFuel > 0)
                        {
                            jetpackEnabled = true;
                        }
                        // If player has no rocket fuel, disable the jetpack
                        else if (playerMovement.rocketFuel <= 0)
                        {
                            jetpackEnabled = false;
                        }
                    }
                    break;

                // Record end touch position and check if tap or directional swipe
                case TouchPhase.Ended:
                    endTouch = touch.position;
                    if (!jetpackEnabled && isGrounded)
                    {
                        // Swipe right
                        if (beginTouch.x < endTouch.x)
                        {
                            // If in left lane or middle lane, move right
                            SideMove("right");
                        }
                        // Swipe left
                        else if (beginTouch.x > endTouch.x)
                        {
                            // If in right lane or middle lane, move left
                            SideMove("left");
                        }
                    }
                    else
                    {
                        jetpackEnabled = false;
                    }
                    break;
            }
        }
    }

    // Move player sideways
    private void SideMove(string direction)
    {
        isMoving = true;
        StartCoroutine(playerMovement.SideMovement(direction));
    }
}
