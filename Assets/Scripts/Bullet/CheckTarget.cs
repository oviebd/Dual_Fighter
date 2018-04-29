using System.Collections;
using UnityEngine;

public class CheckTarget : MonoBehaviour
{
    [SerializeField] private GameObject _forwardObj; // Check for enemy
    [SerializeField] private float _checkForEnemyInradious = 1f; // Check for enemy
    [SerializeField] private float _timeForMovement = 1.0f;

    private bool _isTargetFound = false;                         // Whether bullet set the target.
    public string hitTag { private get; set; } // For Hitting Object Tag

    private GameObject _targetedObj;    // gameobject that target the bullet


    private void Start()
    {
        // _forwardObj.gameObject.SetActive(false);
    }

    void SetDestination()
    {
        if (hitTag != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(hitTag);
            float tempDistance;
            for (int i = 0; i < enemies.Length; i++)
            {
                tempDistance = Vector3.Distance(_forwardObj.transform.position, enemies[i].transform.position);
                if (tempDistance < _checkForEnemyInradious)
                {
                    _isTargetFound = true;
                    _targetedObj = enemies[i];
                }
            }
        }
    }

    void Update()
    {
        if (_isTargetFound == false)
        {
            SetDestination();
            _forwardObj.transform.localPosition += new Vector3(0, 0, 1.0f) * 10f;
        }
        if (_isTargetFound == true)
        {
            Debug.Log("In target check : Target Found : " + _targetedObj.name);
            gameObject.GetComponentInChildren<Bullet>().targetedObj = _targetedObj;
        }
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(_timeForMovement);
        _isTargetFound = false;
        //   _forwardObj.gameObject.SetActive(false);

    }

    public void Restart()
    {
        transform.localPosition = Vector3.zero;
        //  _forwardObj.gameObject.SetActive(true);
        StartCoroutine(Timeout());
    }
}
