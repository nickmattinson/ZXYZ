using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Declare variables
    [SerializeField] int moveSpeed;
    [SerializeField] float rotationSpeed;
    private Vector2 moveInput;
    // Smooth player movement variables
    [SerializeField] private Rigidbody2D rb2d;
    private float activeMoveSpeed;
    private Vector2 smoothMoveInput;
    private Vector2 movementInputSmoothVelocity;
    // Dash variables
    [SerializeField] float dashSpeed;
    [SerializeField] float dashLength;
    [SerializeField] float dashCooldown;
    [SerializeField] Button dashIcon;
    private float dashCounter;
    private float dashCooldownCounter;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }
    private void FixedUpdate()
    {
        move();
        //rotateTowardsControllerInput();
        rotateTowardsMouse();
        checkDash();
    }
    private void move()
    {
        moveInput = GetMoveInput();
        moveInput.Normalize();
        smoothMoveInput = Vector2.SmoothDamp(smoothMoveInput, moveInput, ref movementInputSmoothVelocity, 0.1f);
        rb2d.velocity = smoothMoveInput * activeMoveSpeed;

        // Stop the player if no input is detected
        if (moveInput == Vector2.zero)
        {
            rb2d.velocity = Vector2.zero;
        }
    }
    private void checkDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash();
        }
        if (dashCounter > 0)
        {
            //dashIcon.interactable = false;
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
                //dashIcon.interactable = true;
            }
        }
        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }
    private void dash()
    {
        if (dashCooldownCounter <= 0 && dashCounter <= 0)
        {
            activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;
            //dashIcon.interactable = true;
        }
    }
    private Vector2 GetMoveInput()
    {
        // Get input from both keyboard and controller
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // If no input from keyboard, check for controller input
        if (moveInput == Vector2.zero && Gamepad.current != null)
        {
            moveInput = Gamepad.current.leftStick.ReadValue();
        }
        return moveInput;
    }
    private void rotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void rotateTowardsControllerInput()
    {
        Vector2 rotationInput = GetRotationInput();
        if (rotationInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(rotationInput.y, rotationInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private Vector2 GetRotationInput()
    {
        // Get input from both keyboard and controller
        Vector2 rotationInput = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));

        // If no input from keyboard, check for controller input
        if (rotationInput == Vector2.zero && Gamepad.current != null)
        {
            rotationInput = Gamepad.current.rightStick.ReadValue();
        }
        return rotationInput;
    }
}