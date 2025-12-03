using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Alien : MonoBehaviour
{
    [Header("Alien Settings")]
    public Sprite[] animationSprites;
    public float animationTime = 1f;
    public int pointValue;

    public System.Action killed;
    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.killed.Invoke();
            AudioManager.instance.PlaySFXClip(3, 0.50f);
            ScoreManager.instance.AddScore(this.pointValue);    
            this.gameObject.SetActive(false);
        }
    }
}

