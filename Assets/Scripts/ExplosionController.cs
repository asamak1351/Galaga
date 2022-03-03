using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationClip clip;

    private void OnEnable()
    {
        Invoke("Disable", clip.length);
    }

    private void Disable()
    {
        Destroy(gameObject);
    }
}
