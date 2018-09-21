using UnityEngine;
using System.Collections;

/// <summary>
/// Obstacle, must be managed by pool manager
/// </summary>
public class Obstacle : PoolObject
{
    // Use this for initialization
    private void Awake()
    {
        this.Type = ObjectType.Obstacle;
        this.IsUsed = false;

    }

    /// <summary>
    /// Handle collision with player
    /// </summary>
    /// <param name="_collision">Collision.</param>
	private void OnCollisionEnter2D(Collision2D _collision)
    {
        Debug.Log("OnCollisionEnter2D --");
        Player player = _collision.transform.GetComponent<Player>();
        if (player != null)
        {
            player.OnCrashed();

        }

    }

    /// <summary>
    /// Handle player jump over the obstacle.
    /// </summary>
    /// <param name="_collision">Collision.</param>
	private void OnTriggerEnter2D(Collider2D _collision)
    {
        Debug.Log("Jump Over +10 score");
        GameManager.Instance.OnJumpOver();

    }
}
