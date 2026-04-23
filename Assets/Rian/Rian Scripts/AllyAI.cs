using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
//asfg

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

    int critChance;

    public Vector2 boxsize;
    private float castDistance;

    float knockbackForce = 100f;
    float damageAmount = 1f;


    [SerializeField] private float speed = 1f;
    [SerializeField] private float topSpeed = 10f;

    private bool punchingAnim;
    private bool playerDetector = false;
    public float detectionRange = 10f;
    private float punchCooldown = 0f;
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
        transform.up = moveDirection;
    }

    private void HandlePunch()
    {
        critChance = (int)Random.Range(0.0f, 20.0f);
        if (critChance <= 1)
        {
            knockbackForce = 800f;
            damageAmount = 10f;
        }
        else if (critChance > 1 && critChance <= 16)
        {
            knockbackForce = 400f;
            damageAmount = 5f;
        }
        else if (critChance > 16)
        {
            knockbackForce = 100f;
            damageAmount = 3f;
        }

        Debug.Log("Mittens has detected a collision.");
        if (target.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            target.gameObject.GetComponent<EnemyAI>().enemyHealth -= damageAmount;
            Vector3 direction = target.gameObject.transform.position - transform.position;
            Debug.Log("Direction of knockback: " + direction.normalized);
            target.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);

            //makes the player play a punching animation when attacking an enemy
                if (gameObject.CompareTag("Player"))
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Punch");
            }
        }
        punchCooldown = .5f;

    }



    private void FixedUpdate()
    {

        if (target)

        {
            Vector3 direction = (target.position - (Vector3)transform.position + offset).normalized;
            moveDirection = direction;

            if (Vector2.Distance(target.position, transform.position) <= 5 && punchCooldown <= 0)
            {
                Debug.Log("sgopjgsjpjspgjspjgpsjgp");
                HandlePunch();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    


    rb.AddForce(moveDirection * speed);
        punchCooldown -= Time.fixedDeltaTime;


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

            if (Vector2.Distance(target.position, transform.position) <= 5 && punchCooldown <= 0)
            {
                Debug.Log("sgopjgsjpjspgjspjgpsjgp");
                HandlePunch();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.DrawWireCube(transform.position + offset + Vector3.down * castDistance, boxsize);
    }
   

}


