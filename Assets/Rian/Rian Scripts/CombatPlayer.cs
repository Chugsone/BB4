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
    
    [SerializeField] private float ReloadTime;


    
    public enum WeaponType
    {
        Revolver,
        Fist,
    }

    private WeaponType currentWeapon;
    private Vector2 movementInput;
    public Vector2 boxsize;
    private Vector2 input;
    public Vector3 offset;

    public float speed = 1f;
    public float topSpeed = 10f;   
    public float castDistance;
    
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public GameObject projectilePrefab;
    public Camera mainCamera;
    public int health = 0;
    public float direction;
    private Vector2 shootDirection;


    public AudioClip ShootFX;
    
  
    public bool GodMode = false;
   
    public int mag;
    public int bullets;

    

   
   
   
    private void Awake()
    {
    
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset + Vector3.down * castDistance, boxsize);
    }

    private void Update()
    {
       

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


    }

    

    public void Aim(InputAction.CallbackContext context)
    {
        if (context.control.device is Mouse)
            AimMouse(context);
        else if (context.control.device is Gamepad)
            AimGamepad(context);

        //makes the gun flip upside down when aiming left
        if (gun.transform.right.x < 0)
        {
            gun.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            gun.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    private void AimMouse(InputAction.CallbackContext context)
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//context.ReadValue<Vector2>());
        shootDirection = mousepos - (Vector2)transform.position;
        //gun.transform.right = mousepos - (Vector2)gun.transform.position;
    }

 
    private void AimGamepad(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() != Vector2.zero)
        {
            shootDirection = context.ReadValue<Vector2>();
        }

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
       if (currentWeapon == WeaponType.Fist)
        {
            Slap();
        }
       
       if (currentWeapon == WeaponType.Revolver)
        {
            Shoot(context);
        }
    }


    private void Slap()
    {
        Debug.Log("Slap swing");
        if (Physics2D.OverlapBox(transform.position + offset + Vector3.down * castDistance, boxsize, 0f))
        {
            Debug.Log("Slap Hit");
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