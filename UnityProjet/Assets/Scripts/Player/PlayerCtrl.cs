using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float _movSpeed;
    float speedX, speedY;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxis("Horizontal") * _movSpeed;
        speedY = Input.GetAxis("Vertical") * _movSpeed;
        _rb.linearVelocity = new Vector2(speedX, speedY).normalized;
        Debug.Log(_rb.linearVelocity);
    }
}
