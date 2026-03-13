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
    [SerializeField] private GameObject spawn;
    [SerializeField] private float ReloadTime;
 

  


    
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
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        gun.transform.right = mousepos - (Vector2)gun.transform.position;
    }

 
    private void AimGamepad(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() != Vector2.zero)
        {
            gun.transform.right = context.ReadValue<Vector2>();
        }

    }

   

    public void Shoot(InputAction.CallbackContext context)
    {
       
        if (context.performed &&  bullets > 0)
        {
            AudioSource.PlayClipAtPoint(ShootFX, transform.position);
            GameObject proj = Instantiate(projectilePrefab, spawn.transform.position, Quaternion.identity);
            
            Projectiles projScript = proj.GetComponent<Projectiles>();
           
            proj.transform.right = gun.transform.right;
           
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