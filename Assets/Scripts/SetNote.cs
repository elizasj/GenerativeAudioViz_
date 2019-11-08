using UnityEngine;
using MidiJack;


public class SetNote : MonoBehaviour
{
    public int noteNumber;

    void Update()
    {
        transform.localScale = Vector3.one * (0.1f + MidiMaster.GetKey(noteNumber));
        transform.Rotate(Vector3.one * (MidiMaster.GetKey(noteNumber)));
    }
}
