using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed;


    [Header("Weapon Settings")]
    public ProjectileLogic laserPrefab;
    public float cooldown;

    [Header("Death Visual Settings")]
    public Sprite deathSprite;
    public float flashTime = 0.7f;
    public float flashSpeed = 0.1f;

    // References  
    private PlayerControls playerControls;
    private SpriteRenderer sr;
    private Sprite normalSprite;

    // Gameplay state  
    private bool laserActive;
    private float lastShot;
    private bool isDying;

    // Screen boundaries  
    private float screenLeft;
    private float screenRight;
    private float playerWidth;

    public System.Action killed;

    void Start()
    {
        playerControls = PlayerControls.instance ?? FindFirstObjectByType<PlayerControls>();

        sr = GetComponent<SpriteRenderer>();
        normalSprite = sr.sprite;

        playerWidth = sr.bounds.extents.x;

        float distanceZ = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceZ)).x + playerWidth;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, distanceZ)).x - playerWidth;
    }

    void Update()
    {
        if (isDying) return;

        HandleMovement();
        HandleShooting();
        ClampPosition();
    }

    private void HandleMovement()
    {
        if (playerControls == null) return;

        Vector3 direction = Vector3.zero;
        if (playerControls.rightPress) direction += Vector3.right;
        if (playerControls.leftPress) direction += Vector3.left;

        transform.position += direction * speed * Time.deltaTime;
    }

    private void HandleShooting()
    {
        if (playerControls == null || !playerControls.actionPress) return;

        if (Time.time - lastShot >= cooldown)
            ShootProjectile();
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, screenLeft, screenRight);
        transform.position = pos;
    }

    private void ShootProjectile()
    {
        if (laserActive) return;

        ProjectileLogic projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        projectile.destroyed += LaserDestroyed;
        laserActive = true;

        AudioManager.instance.PlaySFXClip(0, 0.5f);
        lastShot = Time.time;
    }

    private void LaserDestroyed() => laserActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            killed?.Invoke();
            StartCoroutine(PlayDeathEffect());
            LivesManager.instance.LoseLife();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Alien"))
        {
            killed?.Invoke();
            StartCoroutine(PlayDeathEffect());
            LivesManager.instance.InstaDeath();
        }
    }

    private IEnumerator PlayDeathEffect()
    {
        isDying = true;
        float timer = 0f;

        while (timer < flashTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(flashSpeed);
            timer += flashSpeed;
        }

        sr.enabled = false;
        yield return new WaitForSeconds(0.5f);

        sr.sprite = normalSprite;
        sr.enabled = true;
        isDying = false;
    }  


}
