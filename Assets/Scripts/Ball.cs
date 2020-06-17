using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //config paramaters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xVel = 2f;
    [SerializeField] float yVel = 15f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            lockBallToPaddle();
            launchOnMouseClick();
        } 
    }

    private void launchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
        }
    }

    private void lockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    
}
