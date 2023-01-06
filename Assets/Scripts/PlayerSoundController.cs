using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    // Player
    [SerializeField] private List<AudioClip> playerDeathLavaSounds;
    [SerializeField] private List<AudioClip> playerDeathBulletSounds;
    [SerializeField] private List<AudioClip> playerAttackSound;
    [SerializeField] private AudioClip playerRespawningSound;
    [SerializeField] private AudioClip playerSpawnSound;

    // Mechanisms
    [SerializeField] private AudioClip keySound;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerDeathLavaSound()
    {
        _audioSource.PlayOneShot(playerDeathLavaSounds[Random.Range(0, playerDeathLavaSounds.Count)]);
    }
    
    public void PlayPlayerDeathBulletSound()
    {
        _audioSource.PlayOneShot(playerDeathBulletSounds[Random.Range(0, playerDeathBulletSounds.Count)]);
    }
    
    public void PlayPlayerAttackSound()
    {
        _audioSource.PlayOneShot(playerAttackSound[Random.Range(0, playerAttackSound.Count)]);
    }
    
    public void PlayPlayerRespawningSound()
    {
        _audioSource.PlayOneShot(playerRespawningSound);
        _audioSource.loop = true;
    }
    
    public void PlayPlayerSpawnSound()
    {
        _audioSource.PlayOneShot(playerSpawnSound);
        _audioSource.loop = false;
    }

    public void PlayKeySound()
    {
        _audioSource.PlayOneShot(keySound);
    }
}
