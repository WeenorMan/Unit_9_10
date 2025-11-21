using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerControls playerControls;

    public float speed = 5f;

    void Start()
    {
        playerControls = PlayerControls.instance;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (playerControls != null)
        {
            float moveDirection = 0f;

            if (playerControls.rightPress)
                moveDirection += 1f;
            if (playerControls.leftPress)
                moveDirection -= 1f;

            MovePlayer(moveDirection);
        }
    }

    void MovePlayer(float moveDirection)
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveDirection * speed * Time.fixedDeltaTime;
        transform.position = currentPosition;
    }
}
