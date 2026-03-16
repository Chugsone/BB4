using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectiles : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;

    public float knockback;
    public float speed;
    public float lifetime;
    public float knockbackTime;
    [HideInInspector] public int Damage = 1;
    [HideInInspector] public int pierceCount = 1;
    private Vector2 direction;

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
            Debug.Log("Hit Enemy");
        }
    }
}