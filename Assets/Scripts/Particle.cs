using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    public float force = 10f;
    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * force; 
    }
}
