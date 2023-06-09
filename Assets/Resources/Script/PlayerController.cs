using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector3 flippedScale = new Vector3(-1, 1, 1);
    Vector2 groundNormal = Vector2.up;

    Vector3 originalScale;

    private bool isJumping = false;
    bool isFacingRight;

    public float moveX;
    float moveY;

    private Rigidbody2D rigid;
    public HealthManager healthManager;
    private bool isTouchingSpike = false;
    private bool isTouchingTrap = false;

    float moveSpeed = 5f;
    float jumpForce = 950f;
    public float jumpTimer = 0.1f;

    bool isOnGround;

    public Transform Eye;

    public bool canMove = true;

    public int collectedFragments = 0;

    private bool isTouchingBubble = false;

    public Animator animator;
    private bool wasMoving = false;

    public ArduinoController arduinoController;

    public Vector3 ShadowPositionOffset { get; private set; }
    private Vector3 rightOffset = new Vector3(-2, 2, 0);
    private Vector3 leftOffset = new Vector3(2, 2, 0);

    void Start()
    {
        originalScale = transform.localScale;
        rigid = GetComponent<Rigidbody2D>();
        healthManager = GameObject.Find("Health").GetComponent<HealthManager>();
        animator = GetComponent<Animator>();

        arduinoController = GameObject.FindObjectOfType<ArduinoController>();
        if (arduinoController == null)
        {
            UnityEngine.Debug.LogError("Cannot find an instance of ArduinoController!");
        }

        if (GameManager.Instance == null)
        {
            UnityEngine.Debug.LogError("GameManager instance is not found!");
        }
        else
        {
            healthManager.currentHealth = GameManager.Instance.currentHealth;
            collectedFragments = GameManager.Instance.collectedFragments;
        }
    }

    void Update()
    {
        isOnGround = GroundCheck();

        if (GameManager.Instance == null)
        {
            return;
        }

        Movement();
        Direction();
        Jump();

        // Calculate if the player is moving
        bool isMoving = isOnGround && Mathf.Abs(moveX) > 0.01f;

        // Set Animator parameters
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isHurting", isTouchingSpike || isTouchingTrap);

        if (isJumping || isMoving)
        {
            animator.SetBool("isIdle", false);
        }
        else if (GroundCheck() && Mathf.Abs(rigid.velocity.y) < 0.01f)
        {
            animator.SetBool("isIdle", true);
        }

        if (isTouchingSpike)
        {
            healthManager.TakeDamage(1);
            isTouchingSpike = false;
        }

        if (isTouchingTrap)
        {
            healthManager.TakeDamage(1);
            isTouchingTrap = false;
        }

        GameManager.Instance.currentHealth = healthManager.currentHealth;
        GameManager.Instance.collectedFragments = collectedFragments;

        if (isTouchingBubble)
        {
            BubbleVideoController bubbleVideoController = FindObjectOfType<BubbleVideoController>();
            if (bubbleVideoController != null && !bubbleVideoController.videoPlayer.isPlaying)
            {
                bubbleVideoController.videoPlayer.Play();
                bubbleVideoController.videoPlayer.loopPointReached += bubbleVideoController.OnVideoEnd;
            }
        }

        if (!isJumping && Mathf.Abs(rigid.velocity.y) < 0.01f && Mathf.Abs(rigid.velocity.x) < 0.01f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isMoving", false);
        }

        if (arduinoController.leftPressed)
        {
            UnityEngine.Debug.Log("Arduino: Left Pressed");
        }
        if (arduinoController.rightPressed)
        {
            UnityEngine.Debug.Log("Arduino: Right Pressed");
        }

        //UnityEngine.Debug.Log("Arduino: Left: " + arduinoController.leftPressed + ", Right: " + arduinoController.rightPressed + ", Jump: " + arduinoController.jumpPressed);
    }


    private void Movement()
    {
        if (canMove && isOnGround)
        {
            wasMoving = Mathf.Abs(moveX) > 0.01f;

            if (arduinoController.leftPressed)
            {
                UnityEngine.Debug.Log("Arduino left press detected");
                moveX = -1;
            }
            else if (arduinoController.rightPressed)
            {
                UnityEngine.Debug.Log("Arduino right press detected");
                moveX = 1;
            }
            else
            {
                moveX = Input.GetAxis("Horizontal");
            }

            UnityEngine.Debug.Log("MoveX Value: " + moveX);

            moveY = Input.GetAxisRaw("Vertical");
            rigid.velocity = new Vector2(moveX * moveSpeed, rigid.velocity.y);
            animator.SetBool("isIdle", false);
        }
        if (Mathf.Abs(moveX) < 0.01f)
        {
            animator.SetBool("isMoving", false);
        }
    }



    private void Direction()
    {
        if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ShadowPositionOffset = rightOffset;
        }
        else if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ShadowPositionOffset = leftOffset;
        }
    }

    private void Jump()
    {
        bool jumpInput = Input.GetButtonDown("Jump");
        if (arduinoController != null)
        {
            jumpInput = jumpInput || arduinoController.jumpPressed;
        }

        if (canMove && isOnGround && jumpInput && !isJumping)
        {
            UnityEngine.Debug.Log("Jump press detected");
            isJumping = true;
            rigid.AddForce(Vector2.up * jumpForce);
            StartCoroutine(StopJump());
        }
    }



    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(jumpTimer);
        if (isOnGround)
        {
            isJumping = false;

            // After jump, if player was moving, return to moving state
            if (wasMoving)
            {
                animator.SetBool("isMoving", true);
                animator.SetBool("isIdle", false);
            }
            else
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isIdle", true);
            }
        }

        else
        {
            isJumping = false;
        }
    }


    private bool IsOnSlope()
    {
        float slopeAngle = Vector2.Angle(Vector2.up, groundNormal);
        return isOnGround && slopeAngle > 0 && slopeAngle < 45f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grounding(collision, false);

        if (collision.gameObject.CompareTag("Spikes"))
        {
            isTouchingSpike = true;
        }
        else if (collision.gameObject.CompareTag("Traps"))
        {
            isTouchingTrap = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Grounding(collision, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Grounding(collision, true);

        if (collision.gameObject.CompareTag("Spikes"))
        {
            isTouchingSpike = false;
        }
        else if (collision.gameObject.CompareTag("Traps"))
        {
            isTouchingTrap = false;
        }
    }

    private void Grounding(Collision2D col, bool exitState)
    {
        if (exitState)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                isOnGround = false;
                isJumping = false;
            }
        }
        else
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && isOnGround == false && Vector2.Angle(Vector2.up, col.contacts[0].normal) < 45f)
            {
                isOnGround = true;
                groundNormal = col.contacts[0].normal;
                JumpCancel();
            }
        }
    }

    private bool GroundCheck()
    {
        float extraHeight = 0.1f;
        float raycastDistance = extraHeight + 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(rigid.position, Vector2.down, raycastDistance, LayerMask.GetMask("Terrain"));
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        UnityEngine.Debug.DrawRay(rigid.position, Vector2.down * raycastDistance, rayColor);
        return raycastHit.collider != null;
    }

    private void JumpCancel()
    {
        jumpTimer = 0.1f;
    }

    public void InteractWithTower()
    {
        collectedFragments++;

        // Update the method name here
        FindObjectOfType<FragmentsManager>().UpdateFragmentsCount(collectedFragments);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            isTouchingBubble = true;
        }
    }

    public bool IsJumping
    {
        get { return isJumping; }
    }
}