using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.VisualScripting;
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

    int critChance = (int)Random.Range(0.0f, 10.0f);


    [SerializeField] ParticleSystem Blood;

    public float enemyHealth = 3f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float topSpeed = 10f;

    float knockbackForce = 100f;
    float damageAmount = 1f;

    private bool playerDetector = false;
    public float detectionRange = 10f;
    [SerializeField] private LayerMask playerLayer;
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
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            FindFirstObjectByType<CinemachineTargetGroup>().RemoveMember(transform);
        }

        transform.up = moveDirection;

    }
    private void OnCollisionEnter2D(Collision2D collision)
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
        if (collision.gameObject.CompareTag("Player"))
        {
            //makes the enemy play a punching animation when in range with the player
            if (CompareTag("Enemy"))
            {
                GetComponent<Animator>().SetTrigger("Punch");
            }

            Debug.Log("Mittens has detected a collision with the player.");
            if (collision.gameObject.TryGetComponent<AllyAI>(out AllyAI ally))
            {
               


                Vector3 direction = collision.gameObject.transform.position - transform.position; ally.allyHealth -= damageAmount;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
            }
            else if (collision.gameObject.TryGetComponent<CombatPlayer>(out CombatPlayer combatPlayer))
            {
                Vector3 direction = collision.gameObject.transform.position - transform.position;
                combatPlayer.health -= damageAmount;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
            }
            else
            {
                Debug.LogWarning("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }

    }

    public void PlayBlood()
    {
        Blood.Play();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * speed);


        detectTimer -= Time.fixedDeltaTime;

        if (detectTimer <= 0f)
        {
            detectTimer = detectCooldown;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);
            List<Transform> paul = new();
           

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Player"))
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
            
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

   
}


