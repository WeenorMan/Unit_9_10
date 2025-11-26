using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyScript : MonoBehaviour
{
    MoverScript moverScript;
    bool dead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dead = false;
        moverScript = GameObject.Find("Mover").GetComponent<MoverScript>();
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
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            dead = true;
            
            moverScript.enemyCount--;
        }
    }
}
