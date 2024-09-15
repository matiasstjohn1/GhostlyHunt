using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] int soundActive;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        AudioManager.instance.StopSounds();
        AudioManager.instance.PlaySound(soundActive);
    }
}
