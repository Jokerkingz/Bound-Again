using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tile_Animation : MonoBehaviour {
    public static float vYStartingOffset = -20;
    public static float vMinDelay = 0f;
    public static float vMaxDelay = 2f;
    static Scr_DungeonEngine_Controller cDEC;
    private float vYGoal;
    public float vYCurrent;
    private float vSpeed;

    public GameObject vAttachedObject;
    public float vAttachedYCurrent;
    private float vAttachedSpeed;

    
    void Start ()
    {
        if (cDEC == null)
            cDEC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_DungeonEngine_Controller>();
        cDEC.vBlockCount++;
        //Scr_DungeonEngine_Controller tcDEC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scr_DungeonEngine_Controller>();
        vYGoal = transform.position.y;
        Vector2 tSelf = new Vector2(transform.position.x, transform.position.z);
        float tDistance = Vector2.Distance(tSelf, cDEC.vTileAnimationSpot);
        vYCurrent = vYStartingOffset + Random.Range(vMinDelay, vMaxDelay) - (tDistance*7.5f);// - ;
        transform.position = new Vector3(transform.position.x, vYCurrent, transform.position.z);
        
        if (vAttachedObject != null)
        {
            vAttachedYCurrent = vYStartingOffset;
            vAttachedYCurrent = vYCurrent;
            vAttachedObject.transform.position = transform.position;
        }
    }
	
	void Update () {
        float tDistance = vYGoal - vYCurrent;
        if (tDistance > 0)
            vSpeed += 3f*Time.deltaTime;// 2f
        if (tDistance < 0)
            vSpeed -= 3f * Time.deltaTime;
        vSpeed = Mathf.Clamp(vSpeed, -2f, 2f);// was 1f
        if ((vSpeed < .095f && vSpeed > -.095f) && (tDistance > -.095f && tDistance < .095f))
        {
            vSpeed = 0f;
            vYCurrent = vYGoal;
            transform.position = new Vector3(transform.position.x, vYCurrent, transform.position.z);
            if (vAttachedObject == null)
            {
                this.enabled = false;
                cDEC.vBlockCount--;
            }
        }
        vSpeed *= .9f;
        vYCurrent += vSpeed;
        transform.position = new Vector3(transform.position.x, vYCurrent, transform.position.z);

        if (vAttachedObject != null)
            MoveAttachedObject(vAttachedObject,transform.position);
    }

    void MoveAttachedObject(GameObject tAttached, Vector3 tVect3)
    {

        vAttachedSpeed -= Time.deltaTime;
        vAttachedSpeed *= .99f;
        vAttachedYCurrent += vAttachedSpeed;
        tVect3.y += 1f;
        if (vAttachedYCurrent < tVect3.y)
        {
            vAttachedYCurrent = tVect3.y;
            vAttachedSpeed = vSpeed;
            if (vSpeed == 0f && vAttachedSpeed == 0f)
            {
                vAttachedObject = null;
            }
        }
        tAttached.transform.position = new Vector3(transform.position.x, vAttachedYCurrent, transform.position.z);
    }
}
