using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    [SerializeField] ObjectPoolHandler _bulletPool;
    [SerializeField] Transform _gunPos;
    [SerializeField] float _bulletCoolDownTime = 0.5f;
    [SerializeField] GameObject bulletPrefab;

    private float _prevBulletSpawnTime;
    private string _enemyTag;
    private string _fireBtn;

    void Start()
    {
        string tag = gameObject.tag;
        Debug.Log("Gobj Tag " + tag);
        switch (tag)
        {
            case "Player1":
                _enemyTag = "Player2";
                _fireBtn = "space";
                break;

            case "Player2":
                _enemyTag = "Player1";
                _fireBtn = "l";
                break;

        }
        _prevBulletSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_fireBtn))
        {
            if (Time.time - _prevBulletSpawnTime >= _bulletCoolDownTime)
            {
                _prevBulletSpawnTime = Time.time;
                // Debug.Log("Fire Pressed");
                SpawnBullet(_gunPos);
            }


        }
    }



    private void SpawnBullet(Transform gunPos)
    {
        // Get a laser from the pool.
        // GameObject bullet_obj = _bulletPool.GetGameObjectFromPool();
        GameObject bullet_obj = Instantiate(bulletPrefab);
        // Set it's position and rotation based on the gun positon.
        bullet_obj.transform.position = gunPos.position;
        bullet_obj.transform.rotation = gunPos.rotation;

        // Find the FlyerLaser component of the laser instance.
        Bullet bullet = bullet_obj.GetComponentInChildren<Bullet>();

        CheckTarget checkTarget = bullet_obj.GetComponent<CheckTarget>();
        checkTarget.hitTag = _enemyTag;
        //checkTarget.Restart();
        bullet.Restart();
        // Set it's object pool so it knows where to return to.
        /*  bullet.ObjectPool = _bulletPool;
          //  bullet.hitTag = _enemyTag;
          // Restart the laser.
          bullet.Restart();*/


        // Play laser audio.
        // m_LaserAudio.Play();

    }
}
