using System;
using UnityEngine;

public class RBody : MonoBehaviour
{
    public event Action<RaycastHit> Bounced;
    
    public Vector3 Velocity
    {
        get;
        set;
    }

    private void FixedUpdate()
    {
        Move();
        AddForce(Physics.gravity * Time.fixedDeltaTime);
    }

    private void Move()
    {
        var movement = (Velocity * Time.fixedDeltaTime).magnitude;
        
        while (true)
        {
            var ray = new Ray(transform.position, Velocity);
            if (Physics.Raycast(ray, out var hitInfo, movement) && hitInfo.distance < movement)
            {
                transform.position = hitInfo.point;
                
                var bounciness = Mathf.Max(hitInfo.collider.material.bounciness, 0.1f);
                Velocity =  Vector3.Reflect(Velocity, hitInfo.normal) * bounciness;
                
                movement  -= hitInfo.distance;
                Bounced?.Invoke(hitInfo);
            }
            else // no obstacles
            {
                transform.position += Velocity.normalized * movement;
                return;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        Velocity += force;
    }
}