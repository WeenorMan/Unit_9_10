using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject laser;
    PlayerControls playerControls;
    Rigidbody2D rb;

    [Header("Player Settings")]
    public float speed = 5f;

    [Header("Weapon Settings")]
    public float cooldown;
    float lastShot;

    void Start()
    {
        playerControls = PlayerControls.instance;
        rb = GetComponent<Rigidbody2D>();

    }

  

    void Update()
    {
        rb.linearVelocity = new Vector3(0, 0, 0);

        if (playerControls != null)
        {

            if (playerControls.rightPress)
            {
                rb.linearVelocity = new Vector3(1, 0, 0);
            }
            if (playerControls.leftPress)
            {
                rb.linearVelocity = new Vector3(-1, 0, 0);

            }

        }

        ShootProjectile();
    }

    void ShootProjectile()
    {
        

        if(playerControls != null)
        {
            if (playerControls.actionPress)
            {
                if (Time.time - lastShot < cooldown)
                {
                    return;
                }

                GameObject clone;
                clone = Instantiate(laser, transform.position, transform.rotation);


                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

                rb.linearVelocity = new Vector2(0, 5);


                rb.transform.position = new Vector3(transform.position.x, transform.position.y +
                0.5f, transform.position.z + 1);

                lastShot = Time.time;

            }
        }

        
    }
}
