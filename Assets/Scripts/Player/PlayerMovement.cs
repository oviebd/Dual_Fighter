using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int playerNum;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _loookSensitivity = 10f;



    private string _veryticalInput;
    private string _horizontallInput;

    Vector3 _velocity;
    Vector3 _rotation;

    private Rigidbody _rb;
    private Animator _anim;


    private string _move_forward_anim = "move_forward";
    private string _rotation_anim = "move_rotate";
    private string _IsWalk_anim = "IsWalk";


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

    }


    private void Start()
    {
        _veryticalInput = "Vertical" + playerNum;
        _horizontallInput = "Horizontal" + playerNum;
    }


    void Update()
    {
        GetMoveMentInput();
    }

    void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        PerformMovement();
        PerformRotation();
        CheckMoveAnim();
    }


    float moveHorizontal;
    float moveVertical;

    void GetMoveMentInput()
    {
        moveHorizontal = Input.GetAxisRaw(_horizontallInput);
        moveVertical = Input.GetAxisRaw(_veryticalInput);


        if (moveHorizontal == 0 && moveVertical == 0)
        {
            //  Debug.Log("Ho ho zero");
            _velocity = Vector3.zero;
            _rotation = Vector3.zero;
        }
        else
        {
            _velocity = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * _moveSpeed;
            _rotation = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized *
                _loookSensitivity;

        }
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
        //transform.Translate(_velocity, Space.World);
        _rb.velocity = _velocity * _moveSpeed;

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
