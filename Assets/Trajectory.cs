using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    #region Ball's Variables
    public BallControl ball;
    private CircleCollider2D ballCollider;
    private Rigidbody2D ballRigidbody;
    #endregion

    // Shadow ball to be shown at collision point
    public GameObject ballAtCollision;

    

    // Start is called before the first frame update
    void Start()
    {
        ballCollider = ball.GetComponent<CircleCollider2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize trajectory view status, only shown if collide
        bool drawBallAtCollision = false;

        // Offset Hit Point
        Vector2 offsetHitPoint = new Vector2();

        // Take collision point
        RaycastHit2D[] circleCastHit2DArray =
            Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius,
            ballRigidbody.velocity.normalized);

        // For each collision
        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            // If collision happened and not with ball
            if(circleCastHit2D.collider != null &&
                circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                // Trajectory will be drawn from ball's current center to ball's center at collision,
                // Which is a point offset from collision point

                Vector2 hitPoint = circleCastHit2D.point;

                Vector2 hitnormal = circleCastHit2D.normal;

                // Get offset Hit point
                offsetHitPoint = hitPoint + hitnormal * ballCollider.radius;

                // Draw trajectory
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);
            
                // If not side wall, draw the reflection too
                if(circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    // Calculate inVector
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    // Calculate outVector
                    Vector2 outVector = Vector2.Reflect(inVector, hitnormal);

                    // Hitung dot product dari outVector dan hitNormal. Used when the line is not drawn
                    float outDot = Vector2.Dot(outVector, hitnormal);
                    if(outDot > -1.0f && outDot < 1.0)
                    {
                        // Draw the reflection
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f
                            );

                        // To draw "shadow"
                        drawBallAtCollision = true;
                    }
                }

                break;
            
            }
        }

        if(drawBallAtCollision)
        {
            // Draw in offset hit point
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.gameObject.SetActive(true);
        } else
        {
            ballAtCollision.SetActive(false);
        }
    }
}
