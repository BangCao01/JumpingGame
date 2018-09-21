using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Road object, should be able to push to pool, in case the problem be more complexier.
/// </summary>
public class Road : PoolObject
{
    // Use this for initialization
    public enum RoadState
    {
        Stop = 0,
        Go = 1,
    }
    public RoadState State
    {
        set
        {
            this.state = value;
        }

        get
        {
            return this.state;
        }
    }
    //State of road
    private RoadState state;
    //Handle road's position
    private Vector2 roadPosition;
    //Step on moving road
    private const float STEP = -0.1f;
    //List of obstacles
    private List<Obstacle> obstacles;

    /// <summary>
    /// Reset to the begin state of a road.
    /// </summary>
    public void Reset()
    {
        this.roadPosition.x = 16.0f;

        for (int i = 0; i < this.obstacles.Count; i++)
        {
            Obstacle obstacle = this.obstacles[i];
            obstacle.IsUsed = false;
            obstacle.gameObject.transform.parent = null;

        }

        this.obstacles.Clear();

        GenerateObstacle();
    }

    /// <summary>
    /// Scroll the road from right to left.
    /// </summary>
    public void OnGo()
    {

        this.roadPosition.x += STEP;

        if (this.roadPosition.x <= -16.0f)
            Reset();

        this.transform.position = this.roadPosition;


    }


    private void Awake()
    {
        this.Type = ObjectType.Road;
        this.Initialize();
    }

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    private void Initialize()
    {
        this.obstacles = new List<Obstacle>();
        this.roadPosition = this.transform.position;
    }

    /// <summary>
    /// Generates the obstacle.
    /// </summary>
    private void GenerateObstacle()
    {
        //int randType = Random.Range(1,3);

        int numOfObstacle = Random.Range(1, 5);
        int posX = -5;
        int posY = 0;
        for (int i = 0; i < numOfObstacle; i++)
        {
            posX += Random.Range(3, 5);
            posY = Random.Range(0, 4);

            Obstacle obs = PoolManager.Instance.GetAnObstacle();
            obs.transform.parent = this.transform;
            obs.transform.localPosition = new Vector2(posX, posY);
            obs.gameObject.SetActive(true);
            this.obstacles.Add(obs);
        }

    }

}
