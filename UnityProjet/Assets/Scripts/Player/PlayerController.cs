using UnityEngine;

/// <summary>
/// À attacher directement sur le GameObject Player.
/// Gère uniquement le déplacement top-down et la physique du joueur.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Déplacement")]
    [SerializeField] private float _moveSpeed = 4f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _canMove = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_canMove) return;
        ReadInput();
    }

    private void FixedUpdate()
    {
        if (!_canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        MovePlayer();
    }
    private void ReadInput()
    {
        _moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    private void MovePlayer()
    {
        _rb.linearVelocity = _moveInput * _moveSpeed;
    }

    // Active ou désactive le mouvement du joueur.
    public void SetMovement(bool enabled)
    {
        _canMove = enabled;
        if (!enabled) _rb.linearVelocity = Vector2.zero;
    }

    /// Retourne vrai si le joueur est en mouvement.
    public bool IsMoving => _moveInput != Vector2.zero && _canMove;

}
