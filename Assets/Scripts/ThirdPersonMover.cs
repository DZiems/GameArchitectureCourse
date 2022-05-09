using UnityEngine;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 1000f;
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        var mouseMovement = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseMovement * Time.deltaTime * _turnSpeed, 0);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity.Normalize();


        if (Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2f;
            velocity *= 2f;
        }

        velocity *= _moveSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;

        _rigidbody.MovePosition(transform.position + offset);

        //3rd parameter is damping time on transition
        _animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
        _animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
    }
}
