using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : NetworkBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction = Vector2.zero;
    public float speed = 5f;

    [Header("Input")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    public FixedJoystick joystick;
    [Header("Sprites")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }
    private void Start()
    {
        //if (IsOwner)
        //    joystick = FindObjectOfType<FixedJoystick>();
    }
    private void Update()
    {
        if (!IsOwner || !Application.isFocused) return;
        if (UI_Manager.Instance.IsPause()) return;
        //Debug.Log("Moving", gameObject);
        //if (joystick.Direction.y > 0)
        //{
        //    SetDirection(Vector2.up, spriteRendererUp);
        //}
        //if (joystick.Direction.y < 0)
        //{
        //    SetDirection(Vector2.down, spriteRendererDown);
        //}
        //if (joystick.Direction.x < 0)
        //{
        //    SetDirection(Vector2.left, spriteRendererLeft);
        //}
        //if (joystick.Direction.x > 0)
        //{
        //    SetDirection(Vector2.right, spriteRendererRight);
        //}
        //if (joystick.Direction == Vector2.zero)
        //{
        //    SetDirection(Vector2.zero, activeSpriteRenderer);
        //}


        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private async void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        await UniTask.Delay(1250);
        gameObject.SetActive(false);
        GameManager.Instance.FireDieEvent(GetComponent<PlayerController>().idInGame);
        //Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }


}
