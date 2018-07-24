using UnityEngine;

public class RayCastBasedWeapon : MonoBehaviour
{

    [SerializeField] public Transform _gunPos;
    [SerializeField] float _bulletCoolDownTime = 0.5f;
    [SerializeField] GameObject _particlePrefab;

    private int _playerNum;

    private float _prevBulletSpawnTime;
    private string _fireBtn;

    private PlayerWeaponManager _playerWeaponManager;


    private FieldOfViewHelper _fieldOfViewHelper;

    [SerializeField] float viewAngle;
    [SerializeField] float viewRadious;

    void Start()
    {

        _playerWeaponManager = gameObject.GetComponent<PlayerWeaponManager>();
        _fieldOfViewHelper = gameObject.GetComponent<FieldOfViewHelper>();

        _playerNum = _playerWeaponManager.playerNum;

        _prevBulletSpawnTime = Time.time;
        // _fireBtn = "P" + _playerNum + "Attack2";
        _fireBtn = "P" + _playerNum + "Attack2";

        Debug.Log("Raycast " + _fireBtn);
    }


    void Update()
    {
       if (Input.GetButtonDown(_fireBtn))
        {
            if (Time.time - _prevBulletSpawnTime >= _bulletCoolDownTime)
            { 
                _prevBulletSpawnTime = Time.time;
                // Debug.Log("Fire Pressed");
                //SpawnBullet(_gunPos);
                CheckDestination();
            }


        }
    }
    void CheckDestination()
    {
        /*RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_gunPos.position, _gunPos.forward * 100, Color.green);

        if (Physics.Raycast(_gunPos.position, _gunPos.forward, out hit, 100f))
        {

            GameObject hitObj = hit.transform.gameObject;
            RayCastBasedWeapon rayCastBasedWeapon = hitObj.GetComponent<RayCastBasedWeapon>();

            //  if(ray)
            CreateEffect(hitObj);

        }
        else
        {
            //Debug.Log("Hit Not Found ");
        }*/

        Transform hitObjTransform =  _fieldOfViewHelper.GetNearestObj(20, 40);
        if (hitObjTransform != null)
        {
            CreateEffect(hitObjTransform.gameObject);
        }
    }


    void CreateEffect(GameObject hitObj)
    {

        Instantiate(_particlePrefab, hitObj.transform.position, hitObj.transform.rotation);
    }

}
