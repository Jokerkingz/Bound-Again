using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Scr_Editor_PaintObjects : MonoBehaviour {
    public GameObject vPrefab;
    public bool vActive;
    private Vector3 vPreviousV3;
    public GameObject vParentLock;

    public bool vActivateAttachblockAfter;
	// Update is called once per frame
	void Update ()
    {
        if (transform.hasChanged)
        {
            Vector3 tOldVect3 = transform.position;
            Vector3 tCurrentV3;
            tCurrentV3 = new Vector3(Mathf.RoundToInt(tOldVect3.x), Mathf.RoundToInt(tOldVect3.y), Mathf.RoundToInt(tOldVect3.z));
            transform.position = tCurrentV3;
            if (vActive) {
                GameObject tPaintObject;
                GameObject tNewObject;
                if (vPrefab == null)
                    tPaintObject = this.gameObject;
                else
                    tPaintObject = vPrefab;
                if (tCurrentV3 != vPreviousV3)
                {
                    vPreviousV3 = tCurrentV3;
                    tNewObject = Instantiate(tPaintObject);
                    tNewObject.transform.position = tCurrentV3;
                    DestroyImmediate(tNewObject.GetComponent<Scr_Editor_PaintObjects>());
                    DestroyImmediate(tNewObject.GetComponent<Scr_Editor_GridAlign>());
                    if (vParentLock != null)
                    tNewObject.transform.SetParent(vParentLock.transform);
                    if (vActivateAttachblockAfter)
                        tNewObject.AddComponent<Scr_Editor_AttachToBlock>();
                }
                transform.hasChanged = false;
            }
        }
    }
    
}
