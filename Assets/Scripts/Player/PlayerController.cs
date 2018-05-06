using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _loookSensitivity = 10f;

    private float _temp_moveSpeed;
    private float _temp__loookSensitivity;

    private PlayerMotor _motor;
    private string _veryticalInput;
    private string _horizontallInput;
    private string _dodgeInput;

    private bool _isDodgeWorked = false;
    private float _dodgeCoolDownTime = 1f;
    private float _lastDodgeTime;


    private void Start()
    {
        _temp_moveSpeed = _moveSpeed;
        _temp__loookSensitivity = _loookSensitivity;
        _lastDodgeTime = Time.time;


        _motor = GetComponent<PlayerMotor>();

        if (gameObject.tag == "Player1")
        {
            _veryticalInput = "Vertical1";
            _horizontallInput = "Horizontal1";
            _dodgeInput = "f";
        }

        if (gameObject.tag == "Player2")
        {
            _veryticalInput = "Vertical2";
            _horizontallInput = "Horizontal2";
            _dodgeInput = "1";
        }
    }

    void Update()
    {
        //For Type2 Movement
        /* float _xMov = Input.GetAxis(_horizontallInput);
         float _yMov = Input.GetAxis(_veryticalInput);

         Vector3 _moveVertical = transform.forward * _yMov;

         //Final movement vector
         Vector3 _velocity = (_moveVertical).normalized * _moveSpeed;
         _motor.Move(_velocity);

         //Set rotation
          Vector3 _rotation = new Vector3(0.0f, _xMov, 0f) * _loookSensitivity;
         _motor.Rotate(_velocity);*/

        //Type1 Movement
        CheckDodge();
        MoveDirection();

    }

    float moveHorizontal;
    float moveVertical;

    void MoveDirection()
    {
        moveHorizontal = Input.GetAxisRaw(_horizontallInput);
        moveVertical = Input.GetAxisRaw(_veryticalInput);





        //Debug.Log("hor " + moveHorizontal + "   verti :" + moveVertical);
        Vector3 movement;
        Vector3 rotation;
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            //  Debug.Log("Ho ho zero");
            movement = Vector3.zero;
            rotation = Vector3.zero;
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * _moveSpeed;
            rotation = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * _loookSensitivity;
            //  movement = new Vector3(moveVertical, 0.0f, -moveHorizontal).normalized * _moveSpeed;
            //rotation = new Vector3(moveVertical, 0.0f, -moveHorizontal).normalized * _loookSensitivity;
        }

        _motor.Rotate(rotation);
        _motor.Move(movement);


    }

    private void CheckDodge()
    {
        if (Input.GetKeyDown(_dodgeInput) && (Time.time - _lastDodgeTime) >= _dodgeCoolDownTime)
        {
            _isDodgeWorked = true;
            _lastDodgeTime = Time.time;
            _moveSpeed = _moveSpeed * 8;
            _loookSensitivity = _loookSensitivity * 8;
            Debug.Log("Dodge Worked");
        }
        else
        {
            _isDodgeWorked = false;
            _moveSpeed = _temp_moveSpeed;
            _loookSensitivity = _temp__loookSensitivity;
        }
    }
}
