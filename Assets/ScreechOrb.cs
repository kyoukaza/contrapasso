using UnityEngine;

public class ScreechProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifetime = 5f;

    void Start()
    {
        Destroy(gameObject, maxLifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
