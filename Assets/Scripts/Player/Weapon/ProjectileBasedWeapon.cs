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

    void Start()
    {

        _playerWeaponManager = gameObject.GetComponent<PlayerWeaponManager>();
        _playerNum = _playerWeaponManager.playerNum;

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
        RaycastHit hit;

        if (Physics.Raycast(_gunPos.position, _gunPos.forward, out hit, 100f))
        {

            GameObject hitObj = hit.transform.gameObject;
            RayCastBasedWeapon rayCastBasedWeapon = hitObj.GetComponent<RayCastBasedWeapon>();

            InstantiateBullet(hitObj);

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
