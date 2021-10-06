using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoost : MonoBehaviour
{
    public GestureSystem gestureSystem;
    public ParticleSystem ps;

    // Update is called once per frame
    void Update()
    {
        if (gestureSystem.jetpackEnabled && !ps.isPlaying)
        {
            ps.Play();
        }
        else if (!gestureSystem.jetpackEnabled && ps.isPlaying)
        {
            ps.Stop();
        }
    }
}
