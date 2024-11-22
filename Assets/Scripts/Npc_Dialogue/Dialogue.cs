using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string nameNpc;
    public Sprite icon;

    [TextArea(3,20)]
    public string[] sentences;

}
