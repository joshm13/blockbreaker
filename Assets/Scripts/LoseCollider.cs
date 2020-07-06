using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    bool lostLife = false;
    [SerializeField] AudioClip loseLifeSound;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        FindObjectOfType<GameSession>().loseLife();
        if (FindObjectOfType<GameSession>().getLives() <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            lostLife = true;
            AudioSource.PlayClipAtPoint(loseLifeSound, Camera.main.transform.position);
            FindObjectOfType<Ball>().lockBallToPaddle();
            FindObjectOfType<Ball>().launchOnMouseClick();

        }


    }

    public bool getLostLife()
    {
        return lostLife;
    }

    public void setLostLife()
    {
        lostLife = false;
    }

}
