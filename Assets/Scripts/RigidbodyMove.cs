using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    public float Speed = 5f;
    [SerializeField] private Joystick _joystick;
    private Vector2 _moveInput;
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
