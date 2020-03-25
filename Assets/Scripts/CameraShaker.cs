using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Shake(float magnitude, float duration) {

        Vector3 cameraOriginalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration) {

            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, cameraOriginalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = cameraOriginalPos;
    }
}
