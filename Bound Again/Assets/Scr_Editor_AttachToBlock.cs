using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Scr_Editor_AttachToBlock : MonoBehaviour
{
    public Scr_Tile_Animation vPreviousBlock;

    // Use this for initialization
    void Start()
    {
        if (Application.isPlaying)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.hasChanged)
        {
            Debug.Log("Moved");
            Vector3 tOldVect3 = transform.position;
            Vector3 tCurrentV3;
            tCurrentV3 = new Vector3(Mathf.RoundToInt(tOldVect3.x), Mathf.RoundToInt(tOldVect3.y), Mathf.RoundToInt(tOldVect3.z));
            transform.position = tCurrentV3;
            Ray tRay = new Ray(transform.position, Vector3.down);
            RaycastHit tHit;
            if (Physics.Raycast(tRay, out tHit)) {
                //Debug.Log("HIT");
                if (tHit.collider.tag == "Block") {
                    if (vPreviousBlock != null)
                        vPreviousBlock.vAttachedObject = null;
                    vPreviousBlock = tHit.collider.GetComponent<Scr_Tile_Animation>();
                    vPreviousBlock.vAttachedObject = this.gameObject;
                    }
            }

        }
        transform.hasChanged = false;
    }
}