using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dynamic addition to Cinemachine's Target Group was half taken from here: https://forum.unity.com/threads/control-targetgroup-via-script.509943/
public class CameraCritical : MonoBehaviour
{

    [SerializeField] private float _TimeInCameraView = 2f;
    [SerializeField] private float _NewCameraWeight = 1;
    [SerializeField] private float _NewCameraRadius = 2;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StayInCamera());
        Destroy(this, _TimeInCameraView * 2);

    }

    IEnumerator StayInCamera()
    {
        var targetGroup = GameObject.FindObjectOfType<Cinemachine.CinemachineTargetGroup>();

        var targets = targetGroup.m_Targets;
        List<Cinemachine.CinemachineTargetGroup.Target> list = new List<Cinemachine.CinemachineTargetGroup.Target>(targets);

        var newTarget = new Cinemachine.CinemachineTargetGroup.Target();
        newTarget.target = this.transform;
        newTarget.weight = _NewCameraWeight;
        newTarget.radius = _NewCameraRadius;
        list.Add(newTarget);
        targetGroup.m_Targets = list.ToArray();

        yield return new WaitForSeconds(_TimeInCameraView);
        list.Remove(newTarget);
        targetGroup.m_Targets = list.ToArray();


    }


}
