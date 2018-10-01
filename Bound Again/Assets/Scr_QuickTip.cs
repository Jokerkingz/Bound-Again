using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Scr_QuickTip : MonoBehaviour {

    void Update()
    {

        if (transform.hasChanged)
        {
            Vector3 tVector3Align = transform.position;
            tVector3Align.x = Mathf.RoundToInt(tVector3Align.x);
            tVector3Align.y = Mathf.RoundToInt(tVector3Align.y);
            tVector3Align.z = Mathf.RoundToInt(tVector3Align.z);
            transform.position = tVector3Align;
            transform.hasChanged = false;
        }
    }
}
