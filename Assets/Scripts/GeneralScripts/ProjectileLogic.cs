using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    void Update()
    {
        this.transform.position += this.speed * Time.deltaTime * this.direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.destroyed != null)
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
