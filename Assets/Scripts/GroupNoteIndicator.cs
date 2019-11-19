using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class GroupNoteIndicator : MonoBehaviour
{
   float globalVelocity = 0;
   public float t = 0;

    void Update()
    {

        float totalVelocity_ch1 = 0f;
        float totalVelocity_ch2 = 0f;
        float totalVelocity_ch3 = 0f;

        /*
         This script applies incoming MIDI data to all shapes at once.
         Since it's not attached to each shape generated, but the parent,
         we don't have access to noteNumber and must account the number of 
         MIDI notes/shapes that exist
        */

        for (int i = 0; i < 128; i++) // total number of midi notes available
        {
            totalVelocity_ch1 += MidiMaster.GetKey(MidiChannel.Ch1, i); 
            totalVelocity_ch2 += MidiMaster.GetKey(MidiChannel.Ch2, i);
            totalVelocity_ch3 += MidiMaster.GetKey(MidiChannel.Ch3, i);


        }

        foreach (Transform child in transform) // grabbing all children of the parent GameObject & applying MIDI
        {
            child.Rotate(Vector3.one * totalVelocity_ch1);

            Vector3 temp = child.transform.localScale;
            temp.x = MidiMaster.GetKey(MidiChannel.Ch2, (int)totalVelocity_ch2) * 2;
            temp.y = MidiMaster.GetKey(MidiChannel.Ch2, (int)totalVelocity_ch2) * 3;
            temp.z = MidiMaster.GetKey(MidiChannel.Ch2, (int)totalVelocity_ch2) * 4;

        }

       globalVelocity += totalVelocity_ch3;
       globalVelocity = Mathf.Lerp(globalVelocity, 0, t); // using Lerp here to calculate a smooth rotation back to original position
       transform.localEulerAngles = new Vector3(0, 0, globalVelocity);

    }
}