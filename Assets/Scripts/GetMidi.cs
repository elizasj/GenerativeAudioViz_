using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;


public class GetMidi : MonoBehaviour
{
    public GameObject prefab;


    void Start()
        {
            for (var i = 1; i < 2; i++)
            {
                var go = Instantiate<GameObject>(prefab);
                go.transform.position = new Vector3(0, 0, 0);
                go.SetActive(true);

                SetNote setNote = go.AddComponent<SetNote>();
                setNote.noteNumber = 24;
            }
    }

}
