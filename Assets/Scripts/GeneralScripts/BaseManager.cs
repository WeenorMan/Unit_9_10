using System.Collections;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] sprites;

    public int health;
    public System.Action killed;

    private void Start()
    {
        health = sprites.Length - 1;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            this.killed?.Invoke();

            health -= 1;
            UpdateBunkerSprite();
        }
    }

    public void UpdateBunkerSprite()
    {
        if(health > 0)
        {
            sr.sprite = sprites[health];
        }
        else
        {
            AudioManager.instance.PlaySFXClip(3, 0.25f);
            gameObject.SetActive(false);
        }
        
    }
}
