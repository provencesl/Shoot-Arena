using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKBullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 2f;
    [SerializeField] private int impactParticlesAmount = 5;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject impactParticle;
    // Start is called before the first frame update
    void Start() {
        rigidbody2D.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Ground") {
            for(int i = 0; i < impactParticlesAmount; i++) {
                Instantiate(impactParticle, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        } else if(other.tag == "Player") {
            other.GetComponent<PlayerController>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
