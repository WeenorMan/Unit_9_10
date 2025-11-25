using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    PlayerController playerController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= 4)
        {
            Destroy(gameObject);
        }
    }
}
