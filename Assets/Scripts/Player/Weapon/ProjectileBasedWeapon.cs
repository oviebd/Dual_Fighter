using UnityEngine;

public class ProjectileBasedWeapon : MonoBehaviour
{

    [SerializeField] public Transform _gunPos;
    [SerializeField] float _bulletCoolDownTime = 0.5f;
    [SerializeField] GameObject _bulletPrefab;

    private int _playerNum;

    private float _prevBulletSpawnTime;
    private string _fireBtn;

    private PlayerWeaponManager _playerWeaponManager;
    private FieldOfViewHelper _fieldOfViewHelper;

    public float viewRadious;
    [Range(0, 360)]
    public float viewAngle;


    void Start()
    {
        _playerWeaponManager = gameObject.GetComponent<PlayerWeaponManager>();
        _fieldOfViewHelper = gameObject.GetComponent<FieldOfViewHelper>();

        _playerNum = _playerWeaponManager.playerNum;
        Debug.Log("Player Num : "  + _playerNum);
        _prevBulletSpawnTime = Time.time;
        _fireBtn = "P" + _playerNum + "Attack1";
    }


    void Update()
    {
        if (Input.GetButtonDown(_fireBtn))
        {
            if (Time.time - _prevBulletSpawnTime >= _bulletCoolDownTime)
            {
                _prevBulletSpawnTime = Time.time;
                Debug.Log("Fire Pressed");
                //SpawnBullet(_gunPos);
                CheckDestination();
            }
        }
    }


    void CheckDestination()
    {

        Transform hitObjTransform = _fieldOfViewHelper.GetNearestObj(viewRadious, viewAngle);

        if (hitObjTransform != null)
        {
            InstantiateBullet(hitObjTransform.gameObject);
        }
        else
        {

            InstantiateBullet(null);
        }
    }


    void InstantiateBullet(GameObject hitObj)
    {

        GameObject bullet_obj = Instantiate(_bulletPrefab);
        // Set it's position and rotation based on the gun positon.
        bullet_obj.transform.position = _gunPos.position;
        bullet_obj.transform.rotation = _gunPos.rotation;

        ProjectileBullet bullet = bullet_obj.GetComponent<ProjectileBullet>();

        bullet.SetTarget(hitObj);
        bullet.playerNum = _playerNum;

    }
}
