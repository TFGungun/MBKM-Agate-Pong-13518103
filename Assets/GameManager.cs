using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Player 1
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;

    // Player 2
    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;

    // Ball
    public BallControl ball;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    // Max Score
    public int maxScore;

    // Is Debug Window shown?
    private bool isDebugWindowShown = false;

    // Object to draw trajectory prediction
    public Trajectory trajectory;

    // Start is called before the first frame update
    void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    private void OnGUI()
    {
        #region Score GUI
        // Show players' score, Player 1 on top left Player 2 on top right
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        #endregion

        #region Restart Button
        // Restart Button
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            // If the button is pressed, reset scores
            player1.ResetScore();
            player2.ResetScore();

            // And Restart Game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }
        #endregion

        #region Win GUI
        // If player 1 wins -> Reach max score
        if (player1.Score == maxScore)
        {
            // Show text "PLAYER ONE WINS" on left side of screen
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            // And reset ball
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        
        } // If Player 2
        else if (player2.Score == maxScore)
        {
            // Show text "PLAYER TWO WINS" on left side of screen
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            // And reset ball
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);

        }
        #endregion

        #region Debug GUI
        if(GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }


        // If isDebugWindowShown is true, show text for debugs
        if (isDebugWindowShown)
        {
            // Save old GUI color value
            Color oldColor = GUI.backgroundColor;

            // Give new Color
            GUI.backgroundColor = Color.red;

            // Save the physics variables
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            // Show debug text
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " +
                impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " +
                impulsePlayer2Y + ")\n" ;

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            // Get color back
            GUI.backgroundColor = oldColor;

        }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
