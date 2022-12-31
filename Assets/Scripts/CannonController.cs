using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Transform cannonBallSpawn;
    [SerializeField] private float cannonBallSpeed;
    [SerializeField] private float timeBetweenShots;

    private Transform _transform;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        
        StartCoroutine(Shoot());
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
