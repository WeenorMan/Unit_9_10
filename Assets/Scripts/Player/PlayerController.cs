using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerControls playerControls;

    public float speed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls = PlayerControls.instance; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
