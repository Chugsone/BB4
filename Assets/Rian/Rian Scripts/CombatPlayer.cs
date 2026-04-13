using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;




public class CombatPlayer : MonoBehaviour
{
    [SerializeField] public float recoil = 0.5f; 
    [SerializeField] public Rigidbody2D rb; 
    [SerializeField] private GameObject gun;
   
  //  [SerializeField] private Animator animator;
 //   [SerializeField] private Animator gunAnimator;
    
    


    
    public enum WeaponType
    {
        Revolver,
        Fist,
    }

    private WeaponType currentWeapon;
    private Vector2 movementInput;
    public Vector2 boxsize;
    private Vector2 input;
    
    

    [SerializeField] float attackRadius = 1.5f;

    public float speed = 1f;
    public float topSpeed = 10f;   
    public float castDistance;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public GameObject projectilePrefab;
    public Camera mainCamera;
    public float health = 10;
    public float direction;
    public float knockbackForce;
    private Vector2 shootDirection;

    float damageAmount;
    int critChance = (int)Random.Range(0.0f, 10.0f);
    bool canSlap = true;

   public float slapCooldown = 0.5f;

    public LayerMask enemyLayer;


    public AudioClip ShootFX;
    
  
    public bool GodMode = false;
   
    public int mag;
    public int bullets;

    [SerializeField] private Transform spriteTransform;








    private void Awake()
    {
        canSlap = true;
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousepos - (Vector2)transform.position;
        Vector3 offset = direction.normalized * castDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, attackRadius);
    }

    private void Update()
    {

        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (GodMode)
        {
            recoil = 0;
            mag = 100;
            
        }

      
    }

    void FixedUpdate()
    {
       // rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, (movementInput.x * speed));

        // Apply velocity in the FixedUpdate for consistent physics interactions (FixedUpdate is called at a fixed interval)

        rb.AddForce(movementInput * speed);

        if (health <= 0)
        {
            Debug.Log("Player has died.");
            Destroy(gameObject);
        }
    }

    

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//context.ReadValue<Vector2>());
        shootDirection = mousepos - (Vector2)transform.position;

        spriteTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90f);


    }


   

 
   

    public void SwapWeapon(InputAction.CallbackContext context)
    {
        if (currentWeapon == WeaponType.Fist)
        {
            currentWeapon = WeaponType.Revolver;
        }
        else
        {
            currentWeapon = WeaponType.Fist;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {

        if (!context.performed)
        {
            return;
        }

       if (currentWeapon == WeaponType.Fist && canSlap)
        {
            Slap(context);
        }
       
       if (currentWeapon == WeaponType.Revolver)
        {
            Shoot(context);
        }
    }


    private void Slap(InputAction.CallbackContext context)
    {
        Debug.Log("Slap swing");
        canSlap = false;
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousepos - (Vector2)transform.position;
        Vector3 offset = direction.normalized * castDistance;
        StartCoroutine(slapWait());

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRadius, enemyLayer);

        foreach (Collider2D hit in hitEnemies)
        {
            
         
                
                Debug.Log("Hit Enemy");
                EnemyAI enemyAI = hit.GetComponent<EnemyAI>();
            enemyAI.PlayBlood();

                critChance = (int)Random.Range(0.0f, 20.0f);
                if (critChance <= 1)
                {
                    knockbackForce = 1000f;
                    damageAmount = 15f;
                }
                else if (critChance > 1 && critChance <= 16)
                {
                    knockbackForce = 1000f;
                    damageAmount = 10f;
                }
                else if (critChance > 16)
                {
                    knockbackForce = 1000f;
                    damageAmount = 5f;
                }

                Vector3 knockbackDirection = (hit.transform.position - transform.position).normalized * knockbackForce;
                enemyAI.enemyHealth -= 30;

                hit.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection);

        }
    }
    
        
    

    public void Shoot(InputAction.CallbackContext context)
    {
       
        if (context.performed &&  bullets > 0)
        {

            //GameObject proj = Instantiate(projectilePrefab, gun.transform.position, Quaternion.identity);
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            Debug.Log($"Shoot Direction: {shootDirection}");
            GameObject proj = Instantiate(projectilePrefab, gun.transform.position, Quaternion.Euler(0f, 0f, angle));


            Projectiles projScript = proj.GetComponent<Projectiles>();
           
           
            StartCoroutine(GunCooldown());
            bullets -= 1;
           

        //    gunAnimator.SetTrigger("Shoot");
        }
    }

    
    IEnumerator slapWait()
    {
        yield return new WaitForSeconds(slapCooldown);
        canSlap = true;
    }



    public void Move(InputAction.CallbackContext context)
    {
            movementInput = context.ReadValue<Vector2>();
             //  animator.SetFloat("HorizontalSpeed", movementInput.x);
               //animator.SetFloat("VerticalSpeed", movementInput.y);
    }

    IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(recoil);
        
       
    }
    

    public void GodModeEnable(InputAction.CallbackContext context)
    {
      GodMode = true;
    }
}