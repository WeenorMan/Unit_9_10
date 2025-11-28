using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileLogic laserPrefab;
    PlayerControls playerControls;
    private bool laserActive;

    [Header("Player Settings")]
    public float speed;

    [Header("Weapon Settings")]
    public float cooldown;
    float lastShot;

    void Start()
    {
        playerControls = PlayerControls.instance;
    }
  
    void Update()
    {
        if (playerControls != null)
        {
            if (playerControls.rightPress)
            {
                this.transform.position += Vector3.right * this.speed * Time.deltaTime;
            }
            if (playerControls.leftPress)
            {
                this.transform.position += Vector3.left * this.speed * Time.deltaTime;

            }
        }

        if (playerControls != null)
        {
            if (playerControls.actionPress)
            {

                ShootProjectile();

                if (Time.time - lastShot < cooldown)
                {
                    return;
                }
            }
        }
    }

    void ShootProjectile()
    {
        if (!laserActive)
        {
            ProjectileLogic projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            laserActive = true;
            AudioManager.instance.PlaySFXClip(0, 1);
            lastShot = Time.time;
        }
    }

    void LaserDestroyed()
    {
        laserActive = false;
    }
}
