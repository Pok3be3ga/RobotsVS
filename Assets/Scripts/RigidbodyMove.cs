using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    public float Speed = 5f;
    [SerializeField] private Joystick _joystick;
    private Vector2 _moveInput;
    private Vector3 _moveInputKey;
    [SerializeField] private Animator _animator;

    [SerializeField] private Player _player;
    [SerializeField] ChoseRobot _choseRobot;

    private void Start()
    {
        _animator = _choseRobot.Robots[Progress.InstanceProgress.IndexRobot].GetComponentInChildren<Animator>();
    }
    private void Update()
    {

        _moveInput = _joystick.Value.normalized;

        

        if (_moveInput == Vector2.zero)
        {
            _animator.SetBool("Run", false);
        }
        else {
            _animator.SetBool("Run", true);
        }
    }

    private void FixedUpdate()
    {

        float speed = Speed * (1 + _player.MovementSpeed);


        //_rigidbody.AddForce(movement * speed);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        _moveInputKey = new Vector3(moveHorizontal, 0f, moveVertical);

        _rigidbody.velocity = new Vector3(_moveInputKey.x, 0, _moveInputKey.y) * speed;
        _rigidbody.velocity = new Vector3(_moveInput.x, 0, _moveInput.y) * speed;

        if (_rigidbody.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity * Speed, Vector3.up);
        }
    }

    private void OnDisable()
    {
        _animator.SetBool("Run", false);
    }

}
