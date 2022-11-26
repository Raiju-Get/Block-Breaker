
using UnityEngine;

public class block : MonoBehaviour
{

    //config paramaters
    [SerializeField] AudioClip blockDestorySound;
    [SerializeField] GameObject blockParticleSystem;
  //  [SerializeField] int MaxHits;
    [SerializeField] Sprite[] hitSprites;
    //cached reference
    Level level;
    GameSession gameStatus;

    //State variable
    [SerializeField] int timeHits;// For debugging propose to see the hit times
    private void Start()
    {
        CountBreakableBlocks();

    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();

        }
        else
        {
            //do nothing
        }

    }

    private void HandleHit()
    {
        timeHits++;
        int maxHits = hitSprites.Length + 1;
        if (timeHits >= maxHits)
        {
            DestoryBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteindex = timeHits - 1;
        if (hitSprites[spriteindex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteindex];
        }
        else
        {
            Debug.Log("Error: No block Found");
            Debug.Log(gameObject.name);
        }
        
    }

    private void DestoryBlock()
    {
        BlockDestorySound();
        gameStatus.AddToScore();
        TriggerVfx();
        Destroy(gameObject);
    }

    private void BlockDestorySound()
    {
        AudioSource.PlayClipAtPoint(blockDestorySound, Camera.main.transform.position, 0.5f);
        level.BlockDestoryed();
    }

    void TriggerVfx()
    {
        GameObject sparkle = Instantiate(blockParticleSystem, transform.position, transform.rotation);
        Destroy(sparkle,2f);
    }
}
