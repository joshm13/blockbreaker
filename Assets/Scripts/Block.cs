using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;

    //cached reference
    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.countBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroyBlock();
    }

    private void destroyBlock()
    {
        FindObjectOfType<GameSession>().addToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.blockDestroyed();
        
    }
}
