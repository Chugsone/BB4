using JetBrains.Annotations;
using NUnit.Framework.Internal.Filters;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectiles : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;

    //[SerializeField] ParticleSystem Blood;

    public float speed;
    public float lifetime;
    float knockbackForce = 2000;
    public int Damage = 1;
  
    private Vector2 direction;
    private EnemyAI enemyAI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        direction = new (Mathf.Cos(angle), Mathf.Sin(angle));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        

        if (collision.CompareTag("Enemy"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false; //
            Debug.Log("Hit Enemy");

            enemyAI = collision.GetComponent<EnemyAI>();
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized * knockbackForce;
            collision.GetComponent<Rigidbody2D>().AddForce(knockbackDirection);
            Debug.Log((collision.transform.position - transform.position).normalized * knockbackForce);

            enemyAI.enemyHealth -= Damage;
            enemyAI.PlayBlood();
            Destroy(gameObject);
        }
    }
}