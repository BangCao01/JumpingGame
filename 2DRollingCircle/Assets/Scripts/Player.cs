using UnityEngine;
using System.Collections;

/// <summary>
/// Player., attacht to circle game object.
/// </summary>
public class Player : MonoBehaviour
{
    //State of player
    public enum PlayerState
    {
        Alive = 1,
        Dead = 0,
        Go = 2
    }

    //Public accessor
    public PlayerState State
    {
        get
        {
            return this.state;
        }
        set
        {
            this.state = value;
        }

    }
    private PlayerState state;
    private Rigidbody2D body;
    private Vector2 jumpForce;
    private float angle = -4;
    private Vector3 asix = new Vector3(0, 0, 1);

    /// <summary>
    /// Sets the state.
    /// </summary>
    /// <param name="_state">State.</param>
    public void SetState(PlayerState _state)
    {
        this.state = _state;
    }

    /// <summary>
    /// Ons the go.
    /// </summary>
    public void OnGo()
    {
        this.State = PlayerState.Go;
    }

    /// <summary>
    /// OnCrashed, revoke when player hit the obstacle.
    /// </summary>
    public void OnCrashed()
    {
        this.State = PlayerState.Dead;
        GameManager.Instance.OnGameOver();
        Debug.Log("CRASSSEDDDD");
    }

    /// <summary>
    /// Jump, revoke as player touching to the screen.
    /// </summary>
    public void Jump()
    {
        jumpForce.y = Random.Range(100, 400);
        this.body.AddForce(jumpForce);
    }

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    private void Initialize()
    {
        this.State = PlayerState.Alive;
        this.jumpForce = Vector2.zero;
        this.body = this.GetComponent<Rigidbody2D>();

    }

    //Update is called once per frame
    private void Update()
    {
        if (this.state == PlayerState.Go)
        {
            this.transform.Rotate(asix, angle);
        }
    }

}
