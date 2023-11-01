using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightset : MonoBehaviour{
    // Start is called before the first frame update

    private Light theLight;

    private float targetlntensity;
    private float currentlntensity;
    
    void Start()
    {
        theLight = GetComponent<Light>();
        currentlntensity = theLight.intensity;
        targetlntensity = Random.Range(0.4f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(targetlntensity - currentlntensity) >= 0.01)
        {
            if (targetlntensity - currentlntensity >= 0)
                currentlntensity += Time.deltaTime * 3f;
            else
                currentlntensity -= Time.deltaTime * 3f;

            theLight.intensity = currentlntensity;
        }
        else
        {
            targetlntensity = Random.Range(0.4f, 2.5f);
        }
    }
}
