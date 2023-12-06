using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject stonePrefab;
    
    public Sprite normalSprite;
    public Sprite shootingSprite;
    
    
    private SpriteRenderer _playerSpriteRenderer;
    private bool _isShooting = false;
        
    void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSpriteToShooting();
            ShootStone();
            
            Invoke("ChangeSpriteToNormal", 0.1f);
            _isShooting = false;
        }
    }

    void ShootStone()
    {
        _isShooting = true;
        // Instantiate a new stone at the player's position
        GameObject stone = Instantiate(stonePrefab, transform.position + new Vector3(0.7f, -0.2f, 0), Quaternion.identity);
    }
    
    void ChangeSpriteToNormal()
    {
        if (!_isShooting)
        {
            _playerSpriteRenderer.sprite = normalSprite;
        }
    }
    
    void ChangeSpriteToShooting()
    {
        _playerSpriteRenderer.sprite = shootingSprite;
    }
}