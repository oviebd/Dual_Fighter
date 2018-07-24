using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{

    [HideInInspector] GameObject targetedObj;

    [HideInInspector] public int playerNum; //Which player shoot the bullet

    [SerializeField] private float _movingSpeed = 10f;
    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private float _destroyTime = 5.0f;

    private bool _isCollided =false;
    private bool _isMoving;
    private Rigidbody _rb;

    public int damage = 20;

   
    void Start()
    {
        Destroy(gameObject, _destroyTime);
        // _isMoving = false;
        _rb = GetComponent<Rigidbody>();

    }


    public void SetTarget(GameObject target)
    {

        targetedObj = target;
        _isMoving = true;
       // Debug.Log("Target set : is moving" + _isMoving);
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
        if (_isCollided == false)
            _isCollided = true;
        else
            return;

        if (other.GetComponent<PlayerWeaponManager>() != null)
        { 
            //Check Is it the owner of the player
            if (other.GetComponent<PlayerWeaponManager>().playerNum == playerNum)
                return;
        }

        Damageable damagable = other.GetComponent<Damageable>();
        if(damagable != null){

            damagable.particleEffect = _hitParticle;
            damagable.hitObj = gameObject;
            damagable.Damage(damage);
       
            Debug.Log("Damageable Found ");
        }

    }
}
