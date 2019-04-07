using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearOMeter : MonoBehaviour
{
	[SerializeField]
    private Transform _bear;

    [SerializeField]
    private Image _timerBar;

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private Transform _endPoint;

    private void FixedUpdate()
    {
        if (_bear == null) return;
        float length = _endPoint.position.x - _startPoint.position.x;
        float bearRelativeToStart = _endPoint.position.x - (_bear.position.x);
        float percentage = bearRelativeToStart/length;		// TODO : Really don't think this is the right math
        _timerBar.fillAmount = percentage;
    }
}
