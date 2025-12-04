using System;
using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    // ---------------------------------------------------------
    // Inspector Settings
    // ---------------------------------------------------------

    [Header("Alien Settings")]
    [Tooltip("Frames used for the alien's simple 2-frame animation.")]
    public Sprite[] animationSprites;

    [Tooltip("Sprite shown when the alien is killed.")]
    public Sprite deathSprite;

    [Tooltip("Time between animation frame swaps.")]
    public float animationTime = 1f;

    [Tooltip("Points awarded when this alien is killed.")]
    public int pointValue;

    // ---------------------------------------------------------
    // Exposed Events
    // ---------------------------------------------------------

    public Action killed;

    // ---------------------------------------------------------
    // Private Fields
    // ---------------------------------------------------------

    private SpriteRenderer spriteRenderer;
    private int animationFrame = 0;

    // ---------------------------------------------------------
    // Unity Events
    // ---------------------------------------------------------

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    // ---------------------------------------------------------
    // Animation
    // ---------------------------------------------------------

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= animationSprites.Length)
            animationFrame = 0;

        spriteRenderer.sprite = animationSprites[animationFrame];
    }

    // ---------------------------------------------------------
    // Collision Handling
    // ---------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Laser"))
        {
            HandleLaserHit();
        }
        else if (layer == LayerMask.NameToLayer("Bottom"))
        {
            LivesManager.instance.InstaDeath();
        }
    }

    private void HandleLaserHit()
    {
        CancelInvoke(nameof(AnimateSprite));

        spriteRenderer.sprite = deathSprite;

        ScoreManager.instance.AddScore(pointValue);
        AudioManager.instance.PlaySFXClip(3, 0.50f);

        killed?.Invoke();

        StartCoroutine(DeactivateAfterSeconds(0.3f));
    }

    // ---------------------------------------------------------
    // Death Handling
    // ---------------------------------------------------------

    private IEnumerator DeactivateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
