using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Game manager, manage all activities about the game
/// </summary>
public class GameManager : MonoBehaviour
{
    // List of road
    public List<Road> Roads;
    // Handle the player
    public Player mainPlayer;
    public GameObject pauseMenu;
    public GameObject gameoverMenu;
    public GameObject startMenu;
    public GameObject topMenu;
    // Shadow background of menu
    public GameObject menuBg;
    // Score label on HUD.
    public List<Text> scoreLabels;
    // Username of player., random.
    private string userName;
    public enum GameState
    {
        Start = 0,
        Playing = 1,
        Pause = 2,
        Over = 3,

    }
    // Handle states of the game.
    private GameState state;
    // Score of player
    private int score;
    // Single instance
    private static GameManager instance = null;
    // Singleton
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();

            }
            return instance;
        }
    }

    /// <summary>
    /// Click on pause button.
    /// </summary>
    public void OnClickPauseBtn()
    {
        this.state = GameState.Pause;
        ShowMenu();
    }

    /// <summary>
    /// Click on play button.
    /// </summary>
    public void OnClickPlayBtn()
    {
        this.state = GameState.Playing;
        ShowMenu();

    }

    /// <summary>
    /// Click on replay button.
    /// </summary>
    public void OnClickReplayBtn()
    {
        Application.LoadLevel("SampleScene");

    }

    /// <summary>
    /// Click on quit button.
    /// </summary>
    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

    /// <summary>
    /// Handle game over event.
    /// </summary>
    public void OnGameOver()
    {
        this.state = GameState.Over;
        HttpManager.Instance.SendScoreToLeaderBoard(this.userName, this.score);
        ShowMenu();
    }

    /// <summary>
    /// Handle jump over obstacle event.
    /// </summary>
    public void OnJumpOver()
    {
        this.AddScore(10);
    }

    private void Awake()
    {
        instance = this;
        this.userName = "User_" + Random.Range(1, 10000).ToString();
    }

    // Update is called once per frame
    private void Update()
    {

        if (this.state == GameState.Playing)
        {
            OnTouch();
            for (int i = 0; i < this.Roads.Count; i++)
            {
                Road road = this.Roads[i];
                road.OnGo();
            }
            UpdateScoreLabel();
        }
    }

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    private void Initialize()
    {
        this.state = GameState.Start;
        ShowMenu();
    }

    /// <summary>
    /// Shows the menu.
    /// </summary>
    private void ShowMenu()
    {
        this.startMenu.SetActive(this.state == GameState.Start);
        this.gameoverMenu.SetActive(this.state == GameState.Over);
        this.pauseMenu.SetActive(this.state == GameState.Pause);
        this.topMenu.SetActive(this.state == GameState.Playing);
        this.menuBg.SetActive(this.state == GameState.Start
                              || this.state == GameState.Over
                              || this.state == GameState.Pause
                             );
        Player.PlayerState playerState = (this.state == GameState.Playing) ? Player.PlayerState.Go : Player.PlayerState.Alive;
        this.mainPlayer.SetState(playerState);
    }

    // In case you want to use my http client.
    private void SaveScoreToLeaderBoardWithMyHttp()
    {
        StartCoroutine(HttpManager.Instance.SendScoreToLeaderBoardWithMyHttp(this.userName, this.score));
    }

    /// <summary>
    /// Adds the score.
    /// </summary>
    /// <param name="_score">Score.</param>
    private void AddScore(int _score)
    {
        this.score += _score;
        ScoreLabel scr = PoolManager.Instance.GetAScoreLabel();
        Vector2 vector = new Vector2(0, 3);
        scr.ShowAt(vector, _score);
        UpdateScoreLabel();
    }

    /// <summary>
    /// Updates the score label.
    /// </summary>
    private void UpdateScoreLabel()
    {
        for (int i = 0; i < this.scoreLabels.Count; i++)
        {
            string scr = this.score.ToString();
            if (i > 0)
                scr = "Total score: " + scr;
            this.scoreLabels[i].text = scr;

        }

    }

    // Handle touch event
    private void OnTouch()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            TouchEnd(pos);
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            TouchBegin(pos);
            return;
        }
        if (Input.GetMouseButton(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            TouchMove(pos);

            return;

        }
    }

    // Handle touch begin
    private void TouchBegin(Vector3 _pos)
    {
        mainPlayer.Jump();
    }

    // Handle touch move
    private void TouchMove(Vector3 _pos)
    {
        
    }

    // Handle touch end
    private void TouchEnd(Vector3 _pos)
    {

    }

}
