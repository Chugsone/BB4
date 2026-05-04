using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public Vector3 offset = new(1, 0);

    private int critChance;


    [SerializeField] ParticleSystem Blood;

    public float enemyHealth = 3f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float topSpeed = 10f;

    private bool punchingAnim;
    float knockbackForce = 100f;
    float damageAmount = 1f;
    private float punchCooldown = 0f;

    public float detectionRange = 10f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectCooldown = .25f;
    private float detectTimer = 0f;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Start()
    {
        //target = GameObject.Find("Player").transform;
        int critChance = (int)Random.Range(0.0f, 20.0f);
    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);

            FindFirstObjectByType<CinemachineTargetGroup>().RemoveMember(transform);
            FindFirstObjectByType<TeamManager>().TeamB.Remove(gameObject);
        }

        transform.up = moveDirection;

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    critChance = (int)Random.Range(0.0f, 20.0f);
    //    if (critChance <= 1)
    //    {
    //        knockbackForce = 800f;
    //        damageAmount = 10f;
    //    }
    //    else if (critChance > 1 && critChance <= 16)
    //    {
    //        knockbackForce = 400f;
    //        damageAmount = 5f;
    //    }
    //    else if (critChance > 16)
    //    {
    //        knockbackForce = 100f;
    //        damageAmount = 3f;
    //    }


    //    Debug.Log("Mittens has detected a collision.");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //makes the enemy play a punching animation when in range with the player
    //        if (CompareTag("Enemy"))
    //        {
    //            animator.SetTrigger("Punch");

    //        }

    //        Debug.Log("Mittens has detected a collision with the player.");
    //        if (collision.gameObject.TryGetComponent<AllyAI>(out AllyAI ally))
    //        {
               


    //            Vector3 direction = collision.gameObject.transform.position - transform.position; ally.allyHealth -= damageAmount;
    //            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
    //        }
    //        else if (collision.gameObject.TryGetComponent<CombatPlayer>(out CombatPlayer combatPlayer))
    //        {
    //            Vector3 direction = collision.gameObject.transform.position - transform.position;
    //            combatPlayer.health -= damageAmount;
    //            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
    //        }
    //        else
    //        {
    //            Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    //        }
    //    }

    //}

    private void HandlePunch()
    {
        
        critChance = (int)Random.Range(0.0f, 20.0f);
        if (critChance <= 1)
        {
            knockbackForce = 1500f;
            damageAmount = 12f;
        }
        else if (critChance > 1 && critChance <= 16)
        {
            knockbackForce = 800f;
            damageAmount = 6f;
        }
        else if (critChance > 16)
        {
            knockbackForce = 200f;
            damageAmount = 4f;
        }


        Debug.Log("Mittens has detected a collision.");
        if (target.gameObject.CompareTag("Player") || target.gameObject.CompareTag("Goon"))
        {
            //makes the enemy play a punching animation when in range with the player
            
                animator.SetTrigger("Punch");

            

            Debug.Log("Mittens has detected a collision with the player.");
            if (target.gameObject.TryGetComponent<AllyAI>(out AllyAI ally))
            {



                Vector3 direction = target.gameObject.transform.position - transform.position; ally.allyHealth -= damageAmount;
                target.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
            }
            else if (target.gameObject.TryGetComponent<CombatPlayer>(out CombatPlayer combatPlayer))
            {
                Vector3 direction = target.gameObject.transform.position - transform.position;
                combatPlayer.health -= damageAmount;
                target.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
            }
            else
            {
                Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }
        punchCooldown = .5f;
    }

   

    public void PlayBlood()
    {
        Blood.Play();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * speed);
        punchCooldown -= Time.fixedDeltaTime;


        detectTimer -= Time.fixedDeltaTime;

        if (detectTimer <= 0f)
        {
            detectTimer = detectCooldown;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);
            List<Transform> paul = new();


            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player") || colliders[i].CompareTag("Goon"))
                {
                    paul.Add(colliders[i].transform); 
                }
            }

            target = paul.OrderBy(t => Vector2.Distance(t.position, transform.position)).FirstOrDefault(); /// paul.OrderBy(v => v.x).FirstOrDefault();


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
    }

   
}


