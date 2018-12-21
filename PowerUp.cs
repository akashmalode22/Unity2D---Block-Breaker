using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    Paddle paddle;
    Ball ball;

    private void Start()
    {
        paddle = FindObjectOfType<Paddle>();
        ball = FindObjectOfType<Ball>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Destroy(gameObject);
        Vector3 resize = new Vector3(2, 0, 0);
        paddle.gameObject.transform.localScale += resize;

    }

}
