using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{

    private Rigidbody _rb;
    private Vector3 _velocity;
    private Vector3 _rotation;
    private Animator _anim;


    private string _move_forward_anim = "move_forward";
    private string _rotation_anim = "move_rotate";
    private string _IsWalk_anim = "IsWalk";

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }


    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }


    public void Rotate(Vector3 rotation)
    {
        _rotation = rotation;
    }

    void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;
        PerformMovement();
        PerformRotation();
        CheckMoveAnim();
    }

    void CheckMoveAnim()
    {
        if (_velocity != Vector3.zero || _rotation != Vector3.zero)
        {
            _anim.SetBool(_IsWalk_anim, true);
        }
        else
        {
            _anim.SetBool(_IsWalk_anim, false);
        }
    }

    void PerformMovement()
    {
        transform.Translate(_velocity, Space.World);

        //For Type2 movement
        /*  if (_velocity != Vector3.zero)
          {

               _rb.MovePosition(_rb.position + _velocity * Time.deltaTime);
          }*/
    }

    void PerformRotation()
    {

        if (_rotation != Vector3.zero)
        {

            if (_rotation != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_rotation), 1.0F);
                // For type2 movement
                //  _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
            }
        }

    }
}
