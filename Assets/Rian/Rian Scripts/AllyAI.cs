using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class AllyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public Vector3 offset = new(1, 0);
    private Vector2 movementInput;

    public float allyHealth = 3f;



    public Vector2 boxsize;
    private float castDistance;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float topSpeed = 10f;

    private bool playerDetector = false;
    public float detectionRange = 10f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectCooldown = .25f;
    private float detectTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Start()
    {
        //target = GameObject.Find("Player").transform;

    }

    private void Update()
    {
        if (allyHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Mittens has detected a collision.");
        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
           collision.gameObject.GetComponent<EnemyAI>().enemyHealth -= 1;
            Vector3 direction = collision.gameObject.transform.position - transform.position;
            Debug.Log("Direction of knockback: " + direction.normalized);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 10f);
        }

    }

    private void FixedUpdate()
    {
        
        rb.AddForce(moveDirection * speed);



        detectTimer -= Time.fixedDeltaTime;

        if (detectTimer <= 0f)
        {
            detectTimer = detectCooldown;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);
            List<Transform> doug = new();


            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Enemy"))
                {
                    doug.Add(colliders[i].transform);
                }
            }

            target = doug.OrderBy(t => Vector2.Distance(t.position, transform.position)).FirstOrDefault(); /// paul.OrderBy(v => v.x).FirstOrDefault();


        }

        if (target)

        {
            Vector3 direction = (target.position - (Vector3)transform.position + offset).normalized;
            moveDirection = direction;
           
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireCube(transform.position + offset + Vector3.down * castDistance, boxsize);
    }
   

}


