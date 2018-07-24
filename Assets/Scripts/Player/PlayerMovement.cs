using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Setting
    [HideInInspector]  public int playerNum;
    private string _veryticalInput;
    private string _horizontallInput;


    private Rigidbody _rb;

    //Move 
    Vector2 inputData = new Vector2();
    public float walkSpeed = 2;
    public float runSpeed = 6;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    Animator _anim;
    private string _speed_anim = "Speed";


    //Dash
    public float dashSpeed = 2.0f;
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
        inputData = new Vector2(Input.GetAxisRaw(_horizontallInput), Input.GetAxisRaw(_veryticalInput));
        m_DashInput = Input.GetButtonDown(m_Dash);
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (m_DashInput || dashing)
        {
            Dash();
        }
    }

    void MovePlayer()
    {
        
        Vector2 inputDir = inputData.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = (Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg) ;
            // subtract 90 because samurai plane007's rotation is not zero.
            transform.eulerAngles = Vector3.up * (Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg);
            //  transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = (running ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        //float animationSpeed = (running ? 3.0f : 6.5f) * inputDir.magnitude;
        //_anim.SetFloat(_speed_anim, animationSpeed, speedSmoothTime, Time.deltaTime);
    }

  

    void Dash()
    {
        /*if (!dashing)
        {
            dashing = true;

            dash_Direction = new Vector3(inputData.x, 0f, inputData.y).normalized;
            if (dash_Direction == Vector3.zero)
                dash_Direction = transform.forward;

            dash_Time = 0;

            trailRenderer.SetActive(true);

            Debug.Log(m_DashInput);
        }

        dash_Time += Time.deltaTime;


       _rb.velocity = dash_Direction * dash_SpeedMultiplier * dashSpeed;

        if (dash_Time >= dash_Duration && dashing)
        {
            dashing = false;

            _rb.velocity = Vector3.zero;

            trailRenderer.SetActive(false);
        }*/
    }
}
