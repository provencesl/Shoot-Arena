using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFuse : MonoBehaviour
{
    public int fuseInSeconds = 2;
    public float blastRadius = 4f;
    public float damage = 10f;
    public float blastForce = 300f;

    [SerializeField] private GameObject explosionEffect;

    [SerializeField] private GameObject mainCamera;
    //camera shaking amounts
    [SerializeField] private float explosionShakeMagnitude = 0.3f;
    [SerializeField] private float explosionShakeDuration = 0.20f;

    // Start is called before the first frame update
    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StartCoroutine("ExplodeAfterTime");
    }

    private IEnumerator ExplodeAfterTime() {
    while(true) {
        yield return new WaitForSeconds(fuseInSeconds);
        StartCoroutine("Explode");
        }
    }

    private IEnumerator Explode() {
        CreateEffects();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach(Collider2D collider in colliders) {
            Debug.Log(collider.tag);
            if(collider.tag == "Player") {
                collider.GetComponent<PlayerController>().ReceiveDamage(damage);
            }
        }
        yield return new WaitForSeconds(explosionShakeDuration);
        Destroy(gameObject);
    }

    private void CreateEffects() {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        StartCoroutine(mainCamera.GetComponent<CameraShaker>().Shake(explosionShakeMagnitude, explosionShakeDuration));
    }
}
