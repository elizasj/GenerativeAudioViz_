using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phyllotaxis
// https://www.youtube.com/watch?v=KWoJgHFYWxY

public class PhyloPatternGen : MonoBehaviour
{

    int numOfShapes = 0; // object number
    public float scaleOfRadius = 2f; // scaling of radius
    public float pointiness = .3f; // pointiness in z space

    // types of shapes possible
    public GameObject sphere;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        for (int c = 0; c < 16; c++) //channel
        {
            for (int v = 0; v < 8; v++) //note = velocity
            {

                float angle = (i * 137.5f) * Mathf.Rad2Deg; // 137.3 // 137.6
                float radius = scaleOfRadius * Mathf.Sqrt(i);

                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);
                float z = radius * pointiness;

                GameObject shape = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity); // default pos 0,0,0
                shape.name = "shape";
                shape.transform.parent = GameObject.Find("Generator").transform;
                shape.transform.LookAt(Vector3.zero, Vector3.forward);
                shape.SetActive(true);

                // connect shape to Orca
                NoteIndicator noteIndicator = shape.AddComponent<NoteIndicator>();
                noteIndicator.noteNumber = i;

                noteIndicator.channel = c;
                noteIndicator.velocity = v;

                i++;
            }
        }
    }
 
}
