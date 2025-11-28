using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.destroyed.Invoke();
        Destroy(this.gameObject);
    }
}
