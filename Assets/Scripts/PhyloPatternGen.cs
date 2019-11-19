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

                // Golden Ratio / phyllotaxis math
                float angle = (i * 137.5f) * Mathf.Rad2Deg; // 137.3 // 137.6 <-- play with angles to test out different visual patterns
                float radius = scaleOfRadius * Mathf.Sqrt(i);

                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);
                float z = radius * pointiness; 

                // creating all the cubes with Instantiate
                GameObject shape = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity); // default pos 0,0,0
                shape.name = "shape";
                shape.transform.parent = GameObject.Find("Generator").transform; // seting the parent of all cubes to be the GameObject this script is attached to
                shape.transform.LookAt(Vector3.zero, Vector3.forward); // make all cubes direct themselves at (0,0,0)
                shape.SetActive(true); // make cubes visible 

                
                // connect shape to Orca
                NoteIndicator noteIndicator = shape.AddComponent<NoteIndicator>();
                noteIndicator.noteNumber = i; // give each cube a noteNumber (MIDI note number)

                noteIndicator.channel = c; // refers to first loop
                noteIndicator.velocity = v; // resers to second loop
                

                i++;
            }
        }
    }
 
}
