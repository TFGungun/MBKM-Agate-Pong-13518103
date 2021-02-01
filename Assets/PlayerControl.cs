using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Basic Variables
    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;

    // Tombol untuk menggerakkan ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas dan bawah game scene (Batas bawah menggunakan minus(-))
    public float yBoundary = 9.0f;

    // Rigidbody2D raket ini
    private Rigidbody2D rigidbody2D;

    // Skor pemain
    private int score;
    #endregion

    // Last contact point with ball
    private ContactPoint2D lastContactPoint;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        #region Velocity
        // Get current racket's speed
        Vector2 velocity = rigidbody2D.velocity;   
    
        // Add positive velocity is upButton is pressed
        if(Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        // Add negative velocity is downButton is pressed
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        // Zero velocity if not pressing anything
        else
        {
            velocity.y = 0.0f;
        }

        // Insert the velocity back to the RigidBody2D
        rigidbody2D.velocity = velocity;
        #endregion

        #region Boundary

        // Get current position
        Vector3 position = transform.position;

        // If the position passes the boundary, clamp it
        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        } 
        else if(position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        // Insert the position back to Rigidbody2D
        transform.position = position;

        #endregion
    }

    // Increase score by 1 point
    public void IncrementScore()
    {
        score++;
        Debug.Log(score);
    }

    // Reset score to 0
    public void ResetScore()
    {
        score = 0;
    }

    // Score property
    public int Score
    {
        get { return score; }
    }

    // Last Contact Point property
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
