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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
        }
    }
}