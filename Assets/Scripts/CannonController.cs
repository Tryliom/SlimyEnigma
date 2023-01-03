using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Transform cannonBallSpawn;
    [SerializeField] private float cannonBallSpeed;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private GameObject target;

    private Transform _transform;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        
        StartCoroutine(Shoot());
    }
    
    private void Update()
    {
        if (target != null)
        {
            // Rotate the cannon to face the target
            var position = target.transform.position;
            var selfPosition = _transform.position;
            var angle = Mathf.Atan2(position.y - selfPosition.y, position.x - selfPosition.x) * Mathf.Rad2Deg;
            
            _transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            var cannonBallInstance = Instantiate(cannonBall, cannonBallSpawn.position, cannonBallSpawn.rotation);
            cannonBallInstance.GetComponent<BulletProjectile>().SetDirection(GetShootDirection() * cannonBallSpeed);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private Vector2 GetShootDirection()
    {
        var angle = _transform.rotation.eulerAngles.z;
        var x = Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = Mathf.Sin(angle * Mathf.Deg2Rad);
        
        return new Vector2(x, y);
    }
}
