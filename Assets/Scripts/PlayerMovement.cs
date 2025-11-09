using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseXSensitivity = 3.5f;
    [SerializeField] float mouseYSensitivity = 3.5f;
    [SerializeField] float stickXSensitivity = 1.0f;
    [SerializeField] float stickYSensitivity = 0.7f;
    [SerializeField] float stickTurnRateDegPerSec = 140f;
    [SerializeField] float stickDeadzone = 0.2f;
    [SerializeField] float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] string rightX = "RightStickX";
    [SerializeField] string rightY = "RightStickY";
    [SerializeField] float pitchLimit = 89f;

    [SerializeField] float coyoteTime = 0.12f;
    [SerializeField] float jumpBufferTime = 0.12f;
    [SerializeField] float jumpCooldown = 0.08f;

    public float jumpHeight = 2f;

    float velocityY;
    bool isGrounded;

    float pitch;
    float yaw;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    float lastGroundedTime = -999f;
    float lastJumpPressedTime = -999f;
    float lastJumpTime = -999f;
    bool coyoteConsumed = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        yaw = transform.eulerAngles.y;
        pitch = playerCamera.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;
    }

    void Update()
    {
        UpdateLook();
        UpdateMove();
    }

    void UpdateLook()
    {
        float mx = Input.GetAxis("Mouse X") * mouseXSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseYSensitivity;

        float rx = 0f, ry = 0f;
        try { rx = Input.GetAxis(rightX); } catch { }
        try { ry = Input.GetAxis(rightY); } catch { }

        if (Mathf.Abs(rx) < stickDeadzone) rx = 0f; else rx = Mathf.Sign(rx) * (Mathf.Abs(rx) - stickDeadzone) / (1f - stickDeadzone);
        if (Mathf.Abs(ry) < stickDeadzone) ry = 0f; else ry = Mathf.Sign(ry) * (Mathf.Abs(ry) - stickDeadzone) / (1f - stickDeadzone);

        Vector2 stickDegPerFrame = new Vector2(
            rx * stickXSensitivity * stickTurnRateDegPerSec * Time.deltaTime,
            ry * stickYSensitivity * stickTurnRateDegPerSec * Time.deltaTime
        );

        Vector2 mouseDelta = new Vector2(mx, my);
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, mouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        float dx = currentMouseDelta.x + stickDegPerFrame.x;
        float dy = currentMouseDelta.y + stickDegPerFrame.y;

        yaw += dx;
        pitch -= dy;
        pitch = Mathf.Clamp(pitch, -pitchLimit, pitchLimit);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void UpdateMove()
    {
        bool ccGrounded = controller.isGrounded;
        bool sphereGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);
        isGrounded = ccGrounded || sphereGrounded;

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            coyoteConsumed = false;
            if (velocityY < 0f) velocityY = -2f;
        }

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (targetDir.sqrMagnitude > 1f) targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * Time.deltaTime;

        Vector3 v = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;
        controller.Move(v * Time.deltaTime);

        bool jumpDown = Input.GetButtonDown("Jump");
        if (jumpDown) lastJumpPressedTime = Time.time;
        

        bool canCoyote = (Time.time - lastGroundedTime) <= coyoteTime && !coyoteConsumed;
        bool buffered = (Time.time - lastJumpPressedTime) <= jumpBufferTime;
        bool cooldownReady = (Time.time - lastJumpTime) >= jumpCooldown;

        if (buffered && cooldownReady && canCoyote)
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
            lastJumpTime = Time.time;
            coyoteConsumed = true;
            lastJumpPressedTime = -999f;
        }
    }
}
