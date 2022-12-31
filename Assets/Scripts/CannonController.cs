using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Transform cannonBallSpawn;
    [SerializeField] private float cannonBallSpeed;
    [SerializeField] private float timeBetweenShots;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            var cannonBallInstance = Instantiate(cannonBall, cannonBallSpawn.position, cannonBallSpawn.rotation);
            cannonBallInstance.GetComponent<BulletProjectile>().SetDirection(cannonBallSpawn.forward * cannonBallSpeed);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
