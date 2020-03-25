using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private AudioSource weaponAudioSource;
    [SerializeField] private AudioClip rifleFire;
    [SerializeField] private float soundFXVolume = 1f;
    [SerializeField] private GameObject mainCamera;
    
    //camera shaking amounts
    [SerializeField] private float recoilShakeMagnitude = 0.1f;
    [SerializeField] private float recoilShakeDuration = 0.10f;

    public int playerId;

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("Fire" + playerId)) {
            weaponAudioSource.PlayOneShot(rifleFire, soundFXVolume);
            FireWeapon();
        }
    }

    void FireWeapon() {
        StartCoroutine(mainCamera.GetComponent<CameraShaker>().Shake(recoilShakeMagnitude, recoilShakeDuration));
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
