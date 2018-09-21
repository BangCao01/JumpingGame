using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class control score label fly up each time player gain new score  
/// </summary>
public class ScoreLabel : PoolObject
{
    private Text label;
    //the position of the label.
    private Vector2 position;
    //time to fly up.
    private int time;

    //Show the score label at specific position.
    public void ShowAt(Vector2 _position, int _score)
    {
        this.position = Camera.main.WorldToScreenPoint(_position);
        this.label.text = "+" + _score.ToString();
        this.time = 100;
        this.gameObject.SetActive(true);
    }

    private void Awake()
    {
        this.label = this.GetComponent<Text>();
        time = 100;
        this.Type = PoolObject.ObjectType.ScoreLabel;
        this.IsUsed = false;
        this.transform.parent = GameObject.Find("Canvas").transform;
        this.transform.SetAsFirstSibling();

    }

    private void Update()
    {
        if (time > 0)
        {
            time--;
            //Fly up.
            position.y += 1;
            this.transform.position = position;
            if (time == 0)
            {
                this.IsUsed = false;
                this.gameObject.SetActive(false);

            }
        }

    }

}
