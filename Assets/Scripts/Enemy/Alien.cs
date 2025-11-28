using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Alien : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1f;
    bool dead;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        dead = false;
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
            this.gameObject.SetActive(false);
        }
    }
}

