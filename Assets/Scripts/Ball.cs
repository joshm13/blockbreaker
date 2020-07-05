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
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] AudioClip bounceSound;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted || FindObjectOfType<LoseCollider>().getLostLife())
        {
            lockBallToPaddle();
            launchOnMouseClick();
        } 
    }

    public void launchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xVel, yVel);
            FindObjectOfType<LoseCollider>().setLostLife();
        }
    }

    public void lockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityFactor = new Vector2
            (UnityEngine.Random.Range(0f, randomFactor), 
            UnityEngine.Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            //AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
           // myAudioSource.PlayOneShot(clip);
            AudioSource.PlayClipAtPoint(bounceSound, Camera.main.transform.position);
            myRigidBody2D.velocity += velocityFactor;
        }
        
    }


}
