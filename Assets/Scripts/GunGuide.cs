using UnityEngine;

public class GunGuide : MonoBehaviour
{
    [SerializeField] private Cannon       _cannon;
    [SerializeField] private LineRenderer _line;

    private void OnEnable()
    {
        _cannon.DirectionChanged += GenerateGuide;
        _cannon.PowerChanged     += GenerateGuide;
    }
    private void OnDisable()
    {
        _cannon.DirectionChanged -= GenerateGuide;
        _cannon.PowerChanged     -= GenerateGuide;
    }

    private void Start()
    {
        GenerateGuide();
    }

    private void GenerateGuide()
    {
        var direction           = transform.forward;
        var verticalDirection   = Vector3.up;
        var horizontalDirection = direction;
        horizontalDirection.y = 0;
        horizontalDirection.Normalize();
        
        var velocity           = direction * _cannon.GunPower;
        var horizontalVelocity = new Vector2(velocity.x, velocity.z).magnitude;
        var verticalVelocity   = velocity.y;

        const float t = 0.08f;
        
        var point = transform.position;
        
        for (var i = 0; i < _line.positionCount; i++)
        {
            _line.SetPosition(i, point);
            
            point += horizontalDirection * (horizontalVelocity * t);
            point += verticalDirection   * (verticalVelocity   * t - 4.9f * t * t);
            
            verticalVelocity -= 9.8f * t;
        }
    }
}