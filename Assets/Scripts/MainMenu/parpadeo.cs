using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parpadeo : MonoBehaviour
{
    // Pulse light's range between original range
    // and half of the original one

    float duration  = 1.5f;
    float originalRange;

    Light lt;

    void Start()
    {
        lt = GetComponent<Light>();
        originalRange = lt.range;
    }

    void Update()
    {
        var amplitude = Mathf.PingPong(Time.time, duration);

        // Transform from 0..duration to 0.5..1 range.
        amplitude = amplitude / duration * 0.7f + 0.5f;

        // Set light range.
        lt.range = originalRange * amplitude;
    }
}
