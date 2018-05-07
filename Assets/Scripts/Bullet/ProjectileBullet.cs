using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{

    [HideInInspector] GameObject targetedObj;

    [SerializeField] private float _movingSpeed = 10f;

    private bool _isMoving;
    private Rigidbody _rb;


    void Start()
    {
        // _isMoving = false;
        _rb = GetComponent<Rigidbody>();

    }


    public void SetTarget(GameObject target)
    {
        targetedObj = target;
        _isMoving = true;
        Debug.Log("Target set : is moving" + _isMoving);
    }

    void Update()
    {
        if (_isMoving)
        {
            if (targetedObj != null)
                MoveTowardsATarget();
            else
                MoveForward();
        }
    }

    void MoveTowardsATarget()
    {
        float tempDistance = Vector3.Distance(transform.position, targetedObj.transform.position);

        var targetRotation = Quaternion.LookRotation(targetedObj.transform.position - transform.position);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 200f));

        _rb.velocity = transform.forward * _movingSpeed;
    }


    void MoveForward()
    {
        Vector3 p = new Vector3(0, 0, 1);
        _rb.velocity = transform.forward * _movingSpeed;
    }
}
