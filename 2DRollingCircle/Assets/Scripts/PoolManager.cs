using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Pool manager, manage all pool-able objects
/// </summary>
public class PoolManager : MonoBehaviour
{
    //Single instance of pool manager
    private static PoolManager instance = null;
    private List<PoolObject> poolObjects;
    private List<PoolObject> scoreObjects;
    public List<Obstacle> ObstaclePrefabs;
    public ScoreLabel scoreLabelPrefab;

    //Singleton
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PoolManager();

            }
            return instance;
        }
    }

    /// <summary>
    /// Gets an obstacle.
    /// </summary>
    /// <returns>The an obstacle.</returns>
    public Obstacle GetAnObstacle()
    {
        Obstacle ret = null;
        int kind = Random.Range(0, this.ObstaclePrefabs.Count);
        for (int i = 0; i < this.poolObjects.Count; i++)
        {
            PoolObject poolObject = this.poolObjects[i];
            if (!poolObject.IsUsed && poolObject.Type == PoolObject.ObjectType.Obstacle)
            {

                ret = (Obstacle)poolObject;
                break;
            }
        }
        if (ret == null)
        {
            PoolObject newObject = Instantiate(this.ObstaclePrefabs[kind]);
            newObject.Type = PoolObject.ObjectType.Obstacle;
            this.poolObjects.Add(newObject);
            ret = (Obstacle)newObject;
        }

        ret.IsUsed = true;
        return ret;
    }

    /// <summary>
    /// Gets AS core label.
    /// </summary>
    /// <returns>The AS core label.</returns>
    public ScoreLabel GetAScoreLabel()
    {
        ScoreLabel ret = null;

        for (int i = 0; i < this.scoreObjects.Count; i++)
        {
            PoolObject scoreLabelObject = this.scoreObjects[i];
            if (!scoreLabelObject.IsUsed && scoreLabelObject.Type == PoolObject.ObjectType.ScoreLabel)
            {

                ret = (ScoreLabel)scoreLabelObject;
                break;
            }
        }
        if (ret == null)
        {
            PoolObject newObject = Instantiate(this.scoreLabelPrefab);
            newObject.Type = PoolObject.ObjectType.ScoreLabel;
            this.scoreObjects.Add(newObject);
            ret = (ScoreLabel)newObject;
        }

        ret.IsUsed = true;
        return ret;
    }

    /// <summary>
    /// Initialize this instance.
    /// </summary>
	private void Initialize()
    {
        this.poolObjects = new List<PoolObject>();
        this.scoreObjects = new List<PoolObject>();
    }


    private void Awake()
    {
        instance = this;
        this.Initialize();
    }

}
