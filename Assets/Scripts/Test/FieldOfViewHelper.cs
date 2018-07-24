using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewHelper : MonoBehaviour {


   public struct VisibleTargetsStruct
    {
       public float distance;
       public Transform transform;
    }

    [SerializeField]  public float _viewRadious;
    [SerializeField] [Range(0, 360)]  public float _viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<VisibleTargetsStruct> visibleTargetStructList = new List<VisibleTargetsStruct>();

    public void Start()
    {
       // StartCoroutine("FindTargetsWithDebug", 0.2f);
    }


    IEnumerator FindTargetsWithDebug(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets(_viewRadious,_viewAngle);
        }
    }



    void FindVisibleTargets(float viewRadious, float viewAngle)
    {
        visibleTargetStructList.Clear();
       
        Collider[] targetsInViewRadious = Physics.OverlapSphere(transform.position, viewRadious, targetMask);

        for(int i = 0; i < targetsInViewRadious.Length; i++)
        {
            Transform target = targetsInViewRadious[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
               
                float dstToTarget = Vector3.Distance(transform.position, target.position);
              
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    VisibleTargetsStruct visibleTarget;
                    visibleTarget.distance = dstToTarget;
                    visibleTarget.transform = target;

                    visibleTargetStructList.Add(visibleTarget);
                }
            }
        }
    }

    public Transform GetNearestObj(float viewRadious, float viewAngle)
    {
      //  FindVisibleTargets(_viewRadious, _viewAngle);
        FindVisibleTargets(viewRadious,viewAngle);
        Transform nearestTransform = null;
        if(visibleTargetStructList.Count > 0)
        {
            float min_Dis = visibleTargetStructList[0].distance;
            for(int i = 0; i < visibleTargetStructList.Count; i++)
            {
                float curr_dis = visibleTargetStructList[i].distance;

                if (curr_dis<= min_Dis)
                {
                    min_Dis = curr_dis;
                    nearestTransform = visibleTargetStructList[i].transform;
                }
            }
        }

        return nearestTransform;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
