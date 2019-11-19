using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public float t = 0;
    public float result = 0;

    
    // Update is called once per frame
    void Update()
    {
        /*
        LERP - Linear Interpolation 

        The function below interpolates between 1f & 10f by t.
        T is the Lerp. It can be helpful to think of T as the 
        percentage of space between a & b, min & max, 1f & 10f.

        In the inspector, change the value of T to get a better
        feel for what is happening here. T can be any number 
        between 0.0 and 1.0.
        */
      
        result = Mathf.Lerp(1f, 10f, t);

    }
}
