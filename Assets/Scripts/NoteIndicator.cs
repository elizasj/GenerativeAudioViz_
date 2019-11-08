using UnityEngine;
using MidiJack;
using System.Runtime.InteropServices;


public class NoteIndicator : MonoBehaviour
{
    public int noteNumber;

    float initialScale = 1;
    float scale = 0;
    float scaleAnim = 0;
    float scalePerlin = 0;

    public int channel = 0;
    public int velocity = 0;


    private MaterialPropertyBlock propBlock;

    // Start is called before the first frame update
    void Start()
    {
        propBlock = new MaterialPropertyBlock(); // way to use multiple colors with GPU instancing
        initialScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        receiveOrca();
    }

    void receiveOrca()
    {
        // ORCA

        /* 
        MidiMaster.GetKey(channel, noteNumber) 
         
        Returns the velocity value while the key is pressed, or zero
        while the key is released. The value ranges from 0.0 (note-off) 
        to 1.0 (maximum velocity).

        The channel arguments can be omitted. In that case, the 
        functions return the values in the All-Channel slot, 
        which stores mixed status of all active channels.

        Use channels to specify certain actions during live coding 
        For ex,  you know anything you do to channel 0 affects scale,
        anything you do to channel one affects rotation, etc.


        The current MIDI specification calls for 16 MIDI channels. 
        Channels accessed via MidiChannel

        Ch1,    // 0
        Ch2,    // 1
        Ch3,    // ... etc
        Ch4,
        Ch5,
        Ch6,
        Ch7,
        Ch8,
        Ch9,
        Ch10,
        Ch11,
        Ch12,
        Ch13,
        Ch14,
        Ch15, 
        Ch16,
        All   
    
        */

        // CHANNEL 1
        float ch1Velocity = MidiMaster.GetKey(MidiChannel.Ch1, noteNumber);
        var cubes = GameObject.Find("Generator").transform;

        // CHANNEL 2
        scaleAnim += MidiMaster.GetKey(MidiChannel.Ch2, noteNumber) / 2f; // note devided by 10
        if (MidiMaster.GetKey(noteNumber) != 0) Debug.Log(MidiMaster.GetKey(noteNumber));
        scaleAnim -= (scaleAnim - 0) / 5; // fade back to zero
   
        // CHANNEL 3
        foreach (Transform child in cubes)
        {
            child.Rotate(Vector3.one * (MidiMaster.GetKey(MidiChannel.Ch1, noteNumber)));

            Vector3 temp = child.transform.localScale;
            temp.x = MidiMaster.GetKey(MidiChannel.Ch3, noteNumber) * 2;
            temp.y = MidiMaster.GetKey(MidiChannel.Ch3, noteNumber) * 3;
            temp.z = MidiMaster.GetKey(MidiChannel.Ch3, noteNumber) * 4;
        }


        // PERLIN
        scalePerlin = Mathf.PerlinNoise((float)velocity / 10 + Time.time, (float)channel / 10);
        scale = initialScale + scalePerlin + scaleAnim; // original scale, additional waviness, scale from notes from orca

        /*scale += .01f;
        scale = Mathf.Min(scale, initialScale);
        transform.localScale = new Vector3(scale, scale, scale);*/


        //scale = initialScale + scaleAnim;

        transform.localScale = new Vector3(scale, scale, scale);

        //transform.localScale = Vector3.one * (0.1f + MidiMaster.GetKey(noteNumber));
        //transform.localScale = Vector3.one * (scale + MidiMaster.GetKey(noteNumber));

        transform.Rotate(Vector3.one * (MidiMaster.GetKey(MidiChannel.Ch1, noteNumber)));

        float value = (float)(noteNumber - 23) / (128f - 23f);

        var randomColors = Random.ColorHSV(value, value, 1, 1, .5f, .5f);
        var lerpColors = Color.Lerp(randomColors, Color.red, Mathf.PingPong(Time.time, value));


        var perlinColor = Color.Lerp(Color.white, Color.black, scalePerlin); // color from perlin animation

        var color = Color.Lerp(perlinColor, Color.red, scaleAnim); // add red to the perlin animation

        // color = Random.ColorHSV(value, value,1,1,.5f,.5f);
        propBlock.SetColor("_Color", color);
        GetComponent<Renderer>().SetPropertyBlock(propBlock);

    }
}
