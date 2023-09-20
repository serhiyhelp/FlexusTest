using System.Collections;
using System.Linq;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private Cannon         _cannon;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Vector3        _power = Vector3.forward;

    private Coroutine _coroutine;

    private Vector3 _initPos;

    private void OnEnable()
    {
        _cannon.Shot.AddListener(Run);
    }

    private void OnDisable()
    {
        _cannon.Shot.RemoveListener(Run);
    }

    private void Start()
    {
        _initPos = transform.localPosition;
    }

    public void Run()
    {
        IEnumerator Routine()
        {
            for (float t = 0; t < _curve.keys.Last().time; t += Time.deltaTime)
            {
                transform.localPosition = _initPos + _power * _curve.Evaluate(t);
                yield return null;
            }

            transform.localPosition = _initPos;
        }

        if (_coroutine != null) StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Routine());
    }
}