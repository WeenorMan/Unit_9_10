using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ProjectileLogic laserPrefab;
    PlayerControls playerControls;
    ScoreManager scoreManager;
    private bool laserActive;
    public System.Action killed;

    [Header("Player Settings")]
    public float speed;
    public int lives = 2;

    [Header("Weapon Settings")]
    public float cooldown;
    float lastShot;

    void Start()
    {
        if(PlayerControls.instance == null)
        {
            playerControls = FindFirstObjectByType<PlayerControls>();
        }
        else
        {
            playerControls = PlayerControls.instance;
        }
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
                if (Time.time - lastShot < cooldown)
                {
                    return;
                }
                
                ShootProjectile();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            if(this.killed != null)
            {
                this.killed.Invoke();
            }

            lives--;
            print("Lives remaining: " + lives);

            if (lives <= 0)
            {
                SceneManager.LoadScene("Level");
            }
            
        }
    }
}
