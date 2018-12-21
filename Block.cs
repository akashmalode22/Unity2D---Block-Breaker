using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject VFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject powerUp;

    // cached references
    Level level;
    GameStatus gameStatus;

    // state variables
    [SerializeField] int timesHit;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        if (tag == "Breakable")
        {
            HandleHit();
        }
        else if (tag == "Drop") // Drop is power up (1 implemented as of now)
        {
            Destroy(gameObject);
            DropPowerUp();
        }
    }

    private void DropPowerUp()
    {
        GameObject spawnPowerUp = Instantiate(powerUp, transform.position, transform.rotation);
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            Destroy(gameObject);
            level.BlockDestroyed();
            gameStatus.AddToScore();
            TriggerVFX();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is missing from array" + gameObject.name);
        }
    }

    private void TriggerVFX()
    {
        GameObject sparkles = Instantiate(VFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
