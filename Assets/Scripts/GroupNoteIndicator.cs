using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;

public class GroupNoteIndicator : MonoBehaviour
{

    void Update()
    {

        float totalVelocity_ch1 = 0f;
        float totalVelocity_ch2 = 0f;

        for (int i = 0; i < 128; i++) // total number of midi channels available
        {
            totalVelocity_ch1 += MidiMaster.GetKey(MidiChannel.Ch1, i);
            totalVelocity_ch2 += MidiMaster.GetKey(MidiChannel.Ch2, i);


        }
        foreach (Transform child in transform)
        {
            child.Rotate(Vector3.one * totalVelocity_ch1);

            Vector3 temp = child.transform.localScale;
            temp.x = MidiMaster.GetKey(MidiChannel.Ch2, (int)totalVelocity_ch2) * 2;
            temp.y = MidiMaster.GetKey(MidiChannel.Ch2, (int)totalVelocity_ch2) * 3;
            temp.z = MidiMaster.GetKey(MidiChannel.Ch2, (int)totalVelocity_ch2) * 4;


        }
    }
}