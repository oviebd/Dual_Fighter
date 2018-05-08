using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{

    [HideInInspector] GameObject targetedObj;

    [HideInInspector] public int playerNum; //Which player shoot the bullet

    [SerializeField] private float _movingSpeed = 10f;
    [SerializeField] private GameObject _hitParticle;

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
        Vector3 targetPos = new Vector3(targetedObj.transform.position.x, targetedObj.transform.position.y + 1, targetedObj.transform.position.z);

        float tempDistance = Vector3.Distance(transform.position, targetPos);



        var targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 200f));

        _rb.velocity = transform.forward * _movingSpeed;
    }


    void MoveForward()
    {
        Vector3 p = new Vector3(0, 0, 1);
        _rb.velocity = transform.forward * _movingSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        string collObjTag = other.gameObject.tag;

        if (other.GetComponent<PlayerWeaponManager>() != null)
        {
            int playerNumInPlayer = other.GetComponent<PlayerWeaponManager>().playerNum;

            if (playerNumInPlayer != playerNum)
            {
                _rb.isKinematic = true;
                _hitParticle.SetActive(true);
                Destroy(gameObject, 0.1f);
            }

        }

    }
}
