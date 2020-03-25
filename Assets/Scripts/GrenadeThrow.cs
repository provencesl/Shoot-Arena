using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    [SerializeField] private GameObject grenade;
    [SerializeField] private float throwForce = 10f;

    public IEnumerator ThrowGrenade() {
        Debug.Log("Throwing");
        GameObject grenadeInstance = Instantiate(grenade, gameObject.transform.position, gameObject.transform.rotation);
        grenadeInstance.GetComponent<Rigidbody2D>().velocity = transform.right * throwForce;
        yield return null;
    }
}
