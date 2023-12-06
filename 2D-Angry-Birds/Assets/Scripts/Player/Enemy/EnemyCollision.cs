
using UnityEngine;
using TMPro;


public class EnemyCollision : MonoBehaviour
{
    
    public static int EnemyCount = 0;
    public static bool IsGameOver = false;
    
    public Sprite deadSprite;
    
    private SpriteRenderer _enemySpriteRenderer;
    private Rigidbody2D _enemyRigidbody2D;
    private bool _isDead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyRigidbody2D = GetComponent<Rigidbody2D>();
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            Destroy(other.gameObject);
            ChangeSprite();
            // disable rigidbody2d constraints
            _enemyRigidbody2D.constraints = RigidbodyConstraints2D.None;
            EnemyCount++;
            _isDead = true;
        }
        
        if (other.CompareTag("Player"))
        {
            if (!_isDead)
            {
                GameOver();
            }
        }
    }
    
    void ChangeSprite()
    {
        _enemySpriteRenderer.sprite = deadSprite;
    }
    
    void GameOver()
    {
        // stop game 
        Time.timeScale = 0;
        IsGameOver = true;
    }
}
