using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Ball's RigidBody2D
    private Rigidbody2D rigidbody2D;

    // Force amount to push the ball
    public float xInitialForce;
    public float yInitialForce;

    // Ball Tracectory Origin
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;

        // Start game
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        // Reset position
        transform.position = Vector2.zero;

        // Reset velocity
        rigidbody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // Random initial force for Y Axis
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        // Random direction, note : left inclusive left exclusive
        float randomDirection = Random.Range(0, 2);

        // If randomDirection's value is below 1, the ball will move left
        if(randomDirection < 1.0f)
        {
            // Move the ball with force
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce).normalized * 50);
        } else
        {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce).normalized * 50);

        }
    }

    void RestartGame()
    {
        // Reset ball to initial state
        ResetBall();

        // Push ball in 2 seconds
        Invoke("PushBall", 2);


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
