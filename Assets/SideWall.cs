using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // The player that gets the score when ball touches this wall
    public PlayerControl player;

    // Game Manager
    [SerializeField]
    private GameManager gameManager;

    // Will be called when other objects collide with this wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the name is "Ball"
        if(collision.name == "Ball")
        {
            // Add player's score
            player.IncrementScore();
            
            // If not maximal score
            if(player.Score < gameManager.maxScore)
            {
                // Restart Game
                collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
