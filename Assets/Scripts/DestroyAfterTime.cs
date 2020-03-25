using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float secondsToDisappear = 3f;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine("DisappearAfterTime");
    }

    private IEnumerator DisappearAfterTime() {
    while(true) {
        yield return new WaitForSeconds(secondsToDisappear);
        Destroy(gameObject);
        }
    }
}
