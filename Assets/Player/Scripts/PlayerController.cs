using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float smoothTime;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool cursorLocked = true;

    private bool isGrounded => this.controller.isGrounded;
    private float velocity;
    private Vector2 smoothedMove = Vector2.zero;
    private Vector2 smoothedVelocity = Vector2.zero;

    private InputListener inputListener;
    private EventHandler eventHandler;
    private CharacterController controller;

    private void Awake()
    {
        this.controller = GetComponent<CharacterController>();
        this.eventHandler = EventHandler.Instance;
        this.inputListener = InputListener.Instance;        

        if (this.cursorLocked == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnEnable()
    {
        this.eventHandler.JumpInputEvent += this.OnJumpInput;
    }

    private void OnDisable()
    {
        this.eventHandler.JumpInputEvent -= this.OnJumpInput;
    }

    private void Update()
    {
        this.PlayerMove();
    }

    private void PlayerMove()
    {
        if (this.isGrounded && this.velocity < 0)
        {
            this.velocity = 0f;
        }

        Vector2 inputRaw = this.inputListener.GetPlayerMovement();
        
        this.smoothedMove = Vector2.SmoothDamp
            (this.smoothedMove, inputRaw, ref this.smoothedVelocity, this.smoothTime);

        Vector3 move = new Vector3(inputRaw.x, 0f, inputRaw.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        this.controller.Move(move * Time.deltaTime * this.speed);
                
        this.velocity += this.gravity * Time.deltaTime;
        this.controller.Move(new Vector3(0f, this.velocity, 0f) * Time.deltaTime);
    }

    private void OnJumpInput()
    {
        if (this.isGrounded == true)
        {
            this.velocity += Mathf.Sqrt(this.jumpHeight * -2.0f * this.gravity);
        }
    }
}
