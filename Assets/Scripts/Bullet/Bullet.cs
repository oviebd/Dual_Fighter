using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _parentObj;
    [SerializeField] private float _movingSpeed = 10f;
    [SerializeField] private float _bulletLifeDuration = 3f; // How long the laser lasts before it's returned to it's object pool.

    [SerializeField] private GameObject _bulletHit;
    [SerializeField] private AudioSource _spawnAudio;
    //   [SerializeField] private float _checkForEnemyInradious = 1f; // Check for enemy

    private Rigidbody _rb;

    private bool m_Hit;                           // Whether the laser has hit something.
    private bool _isTargetFound = false;                         // Whether bullet set the target.
    private bool _isTargetLost;
    public GameObject targetedObj { private get; set; }           // gameobject that target the bullet
    //  Vector3 _moveVertical;

    public ObjectPoolHandler ObjectPool { private get; set; }          // The object pool the laser belongs to.
                                                                       // public string hitTag { private get; set; } // For Hitting Object Tag


    private float _dummyForwardSpeed = .1f;
    private float _bullet_target_maximum_time = .1f;
    private bool _isBulletMove = false;

    void Start()
    {
        _isTargetLost = false;
        _rb = GetComponent<Rigidbody>();
        _spawnAudio.Play();
        Invoke("ActiveCollisder", .2f);
        Invoke("TargetChecker", _bullet_target_maximum_time);
    }

    void TargetChecker()
    {
        _isBulletMove = true;

    }

    void ActiveCollisder()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    void Update()
    {

        // Debug.Log("Target Found " + targetedObj.name);
        if (targetedObj != null)
        {
            Debug.Log("In Bullet : Target Found : " + targetedObj.name);
            float tempDistance = Vector3.Distance(transform.position, targetedObj.transform.position);

            // while bullet is near the obj then It stop follow obj
            if (tempDistance >= 3.0f && _isTargetLost == true)
            {
                //  _rb.velocity = (targetedObj.transform.position - transform.position).normalized * _movingSpeed;
                // _rb.velocity.y = 0;

                var targetRotation = Quaternion.LookRotation(targetedObj.transform.position - transform.position);
                _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 200f));

            }
            else
                _isTargetLost = true;
        }
        if (m_Hit == false && _isBulletMove)
        {
            _rb.velocity = transform.forward * _movingSpeed;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        m_Hit = true;
        _rb.isKinematic = true;

        _bulletHit.SetActive(true);
        Destroy(_parentObj, 0.1f);
    }

    private IEnumerator Timeout()
    {

        yield return new WaitForSeconds(_bulletLifeDuration);
        Destroy(_parentObj);

    }

    public void Restart()
    {
        // At restart the laser hasn't hit anything.
        m_Hit = false;

        targetedObj = null;
        gameObject.transform.localPosition = Vector3.zero;
        if (_rb != null)
        {
            //  _rb.velocity = Vector3.zero;
        }

        // gameObject.GetComponent<Rigidbody>().isKinematic = true;
        // Start the lifetime timeout.
        StartCoroutine(Timeout());
    }
}

