using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarTransform : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 localScale;
    void Start()
    {
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = ProgressInteract.progress;
        transform.localScale = localScale;
    }

}
