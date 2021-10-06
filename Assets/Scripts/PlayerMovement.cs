using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GestureSystem gestureSystem;
    private Rigidbody rb;
    private readonly float moveSpeed = 10f;
    public float rocketFuel;
    public TextMeshProUGUI rocketText;
    public Animator animator;
    public SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rocketFuel = 0;
    }

    void Update()
    {
        string rt = "Boost: " + ((int)rocketFuel).ToString();
        rocketText.text = rt;
        if (rb.position.y <= -15)
            sceneTransition.StartTransition(2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        if (rocketFuel > 0 && gestureSystem.jetpackEnabled)
        {
            rb.AddForce(transform.up * 0.5f, ForceMode.Impulse);
            if (rb.velocity.y > 15f)
                rb.velocity = new Vector3(rb.velocity.x, 15f, rb.velocity.z);
            rocketFuel -= 0.5f;
        }
    }

    public IEnumerator SideMovement(string direction)
    {
        int lane = Mathf.RoundToInt(transform.position.x);
        float elapsedTime = 0, duration = 0.3f, ratio = elapsedTime / duration;
        Vector3 startPos = transform.position, endPos = startPos;
        switch (direction)
        {
            case "left":
                if (lane >= 0)
                {
                    endPos = startPos - new Vector3(1, 0, -1);
                }
                break;
            case "right":
                if (lane <= 0)
                {
                    endPos = startPos + new Vector3(1, 0, 1);
                }
                break;
            default:
                break;
        }
        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            rb.MovePosition(Vector3.Lerp(startPos, endPos, ratio));
            yield return null;
        }
        gestureSystem.isMoving = false;
    }
}
