using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed=5f;
    public float runSpeed=10f;
    public float g=-9.81f;

    public Slider healthBar;
    public float maxHealth=100f;

    public GameObject deadPanel;
    public GameObject uiPanel;

    [SerializeField] private AudioClip[] sfx;
    private AudioSource sfxPlayer;

    public Slider staminaBar;
    public float maxStamina=100f;
    public float staminaDrain=20f;
    public float staminaRegen=15f;

    public Camera cam;
    public float lookSensitivity=2f;
    public float lookXLimit=85f;

    [SerializeField] private CharacterController characterController;
    private Vector3 velocity;
    private float rotationX=0;
    private float currentStamina;
    private float currentHealth;

    public ZombieSpawner zs;

    private bool isDead=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;

        currentStamina=maxStamina;
        staminaBar.maxValue=maxStamina;
        staminaBar.value=currentStamina;

        currentHealth=maxHealth;
        healthBar.maxValue=maxHealth;
        healthBar.value=currentHealth;

        sfxPlayer=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        float mouseX=Input.GetAxis("Mouse X")*lookSensitivity;
        float mouseY=Input.GetAxis("Mouse Y")*lookSensitivity;

        transform.Rotate(Vector3.up*mouseX);

        rotationX-=mouseY;
        rotationX=Mathf.Clamp(rotationX,-lookXLimit,lookXLimit);
        cam.transform.localRotation=Quaternion.Euler(rotationX,0,0);

        float moveX=Input.GetAxis("Horizontal");
        float moveZ=Input.GetAxis("Vertical");

        bool isMoving=(moveX!=0||moveZ!=0);

        bool isRunning=Input.GetKey(KeyCode.LeftShift) && isMoving && currentStamina>0;

        if (isRunning)
        {
            currentStamina-=staminaDrain*Time.deltaTime;
        }
        else
        {
            if(!Input.GetKey(KeyCode.LeftShift)) currentStamina+=staminaRegen*Time.deltaTime;
        }

        currentStamina=Mathf.Clamp(currentStamina,0,maxStamina);

        staminaBar.value=currentStamina;

        float currentSpeed=isRunning?runSpeed:moveSpeed;

        Vector3 move=transform.right*moveX+transform.forward*moveZ;

        characterController.Move(move*currentSpeed*Time.deltaTime);

        if(characterController.isGrounded && velocity.y < 0)
        {
            velocity.y=-2f;
        }

        velocity.y+=g*Time.deltaTime;
        characterController.Move(velocity*Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        if(isDead) return;

        currentHealth-=amount;
        
        healthBar.value=currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead=true;

        zs.StopGame();

        deadPanel.SetActive(true);
        uiPanel.SetActive(false);

        sfxPlayer.PlayOneShot(sfx[0]);
        characterController.enabled=false;

        Rigidbody rb=gameObject.AddComponent<Rigidbody>();
        rb.mass=50f;

        rb.AddForce(transform.forward*2f,ForceMode.Impulse);
        rb.AddTorque(-transform.right*5f,ForceMode.Impulse);

        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
    }
}
