using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float max = 15f;
    [SerializeField] float min = 2f;

    // cached references
    GameStatus gameStatus;
    Ball ball;

    // Use this for initialization
    void Start () {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), min, max);
        transform.position = paddlePos;
	}

    private float GetXPos()
    {
        if (gameStatus.IsAutoPlayEnabled()) {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

}
