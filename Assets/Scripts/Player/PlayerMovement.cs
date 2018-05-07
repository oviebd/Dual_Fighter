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



    //Dash
    string m_Dash;
    bool m_DashInput;

    bool dashing = false;
    float dash_Time;
    public float dash_Duration = 0.25f;
    public float dash_SpeedMultiplier = 5;
    Vector3 dash_Direction;

    public GameObject trailRenderer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

    }


    private void Start()
    {
        _veryticalInput = "Vertical" + playerNum;
        _horizontallInput = "Horizontal" + playerNum;

        m_Dash = "Dash" + playerNum;
    }


    void Update()
    {

        GetMoveMentInput();

    }

    void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        if (!dashing)
        {
            PerformMovement();
            PerformRotation();
            CheckMoveAnim();
        }

        if (m_DashInput || dashing)
        {
            Dash();
        }


    }


    float moveHorizontal;
    float moveVertical;

    void GetMoveMentInput()
    {
        moveHorizontal = Input.GetAxisRaw(_horizontallInput);
        moveVertical = Input.GetAxisRaw(_veryticalInput);
        m_DashInput = Input.GetButtonDown(m_Dash);

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
        _rb.velocity = _velocity * _moveSpeed;

    }

    void PerformRotation()
    {

        if (_rotation != Vector3.zero)
        {

            if (_rotation != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_rotation), 1.0F);

            }
        }

    }

    void Dash()
    {
        if (!dashing)
        {
            dashing = true;

            dash_Direction = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
            if (dash_Direction == Vector3.zero)
                dash_Direction = transform.forward;

            dash_Time = 0;

            trailRenderer.SetActive(true);

            Debug.Log(m_DashInput);
        }

        dash_Time += Time.deltaTime;


        _rb.velocity = dash_Direction * dash_SpeedMultiplier * _moveSpeed;

        if (dash_Time >= dash_Duration && dashing)
        {
            dashing = false;

            _rb.velocity = Vector3.zero;

            trailRenderer.SetActive(false);
        }
    }
}
