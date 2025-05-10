using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class playermovement : MonoBehaviour
{
    [Header("Footstep Sound")]
    public float walkFootstepInterval = 0.4f;
    public float sprintFootstepInterval = 0.2f;
    private float footstepTimer = 0f;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float rotationSpeed = 10f;
    public float groundDrag = 4f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 15f;
    public float staminaRegenRate = 10f;

    [Header("References")]
    public Transform orientation;
    private Rigidbody rb;
    private Camera playerCamera;
    private StaminaBarUI staminaUI;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    public bool isSprinting = false;
    public bool movementLocked = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerCamera = Camera.main;
        currentStamina = maxStamina;
        staminaUI = FindObjectOfType<StaminaBarUI>();
    }

    void Update()
    {
        HandleInput();
        HandleStamina();
        UpdateStaminaUI();

        rb.linearDamping = groundDrag;
    }

    void FixedUpdate()
    {
        MovePlayer();
        LimitSpeed();

        if (moveDirection.magnitude > 0.1f && rb.linearVelocity.magnitude > 0.1f)
{
    footstepTimer -= Time.deltaTime;

    float interval = isSprinting ? sprintFootstepInterval : walkFootstepInterval;

    if (footstepTimer <= 0f)
    {
        AudioManager.Instance.PlayFootstep();
        footstepTimer = interval;
    }
}
else
{
    footstepTimer = 0f;
}


    }

    void HandleInput()
    {
        if (movementLocked)
    {
        horizontalInput = 0;
        verticalInput = 0;
        return;
    }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f;
    }

    void HandleStamina()
    {
        if (isSprinting)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }

    void UpdateStaminaUI()
    {
        if (staminaUI != null)
            staminaUI.UpdateStaminaBar(currentStamina / maxStamina);
    }

    void MovePlayer()
    {
        float speed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        forward.y = 0f;
        right.y = 0f;

        moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


    void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        float maxSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
