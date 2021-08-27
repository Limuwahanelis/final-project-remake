using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public float ySpeed = 2f;
    public int damage = 5;
    public float duration;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, ySpeed * Time.deltaTime));
    }
    public void SetSpeed(float speed)
    {
        ySpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<IDamagable>().TakeDamage(damage);
    }
}
