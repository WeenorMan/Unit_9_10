using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyScript : MonoBehaviour
{

    bool dead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dead = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "bullet" && dead==false)
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            dead = true;
        }
    }
}
