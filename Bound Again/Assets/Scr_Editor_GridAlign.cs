using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Scr_Editor_GridAlign : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.hasChanged)
        {
            Vector3 tOldVect3 = transform.position;
            transform.position = new Vector3(Mathf.RoundToInt(tOldVect3.x), Mathf.RoundToInt(tOldVect3.y), Mathf.RoundToInt(tOldVect3.z));
            transform.hasChanged = false;

        }

    }
}
