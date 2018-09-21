using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pool object, handle object that may be managed by pool manager.
/// </summary>
public class PoolObject : MonoBehaviour
{
    public enum ObjectType
    {
        Nothing = 0,
        Obstacle = 1,
        Road = 2,
        ScoreLabel,
    };
    //Type of object
    private ObjectType type;
    public ObjectType Type
    {
        get
        {

            return this.type;
        }

        set
        {
            this.type = value;
        }
    }
    //Object is in used or not.
    private bool isUsed;

    public bool IsUsed
    {
        get
        {

            return this.isUsed;
        }

        set
        {
            this.isUsed = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        this.Type = ObjectType.Nothing;
        IsUsed = false;
    }
}
