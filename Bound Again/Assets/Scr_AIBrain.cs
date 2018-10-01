using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AIBrain : MonoBehaviour {
    private GameObject[] vProtagonistList;
    public float vDistance;
    public char tNESW;
    public List<GameObject> tTargets;
    public GameObject vTarget;
    public LayerMask vObstacle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckProtagonist();
        CheckVisible();
    }
    void CheckProtagonist()
    {
        vProtagonistList = GameObject.FindGameObjectsWithTag("Protagonist");
    }
    void CheckVisible()
    {
        List<GameObject> tTargetList = new List<GameObject>();
        //tTargets.Clear();
        Ray tRay;
        //vDistance
        foreach (GameObject tChar in vProtagonistList) {
            {
                tRay = new Ray(transform.position, tChar.transform.position - transform.position);
                if (!Physics.Raycast(tRay, vDistance, vObstacle))
                    tTargetList.Add(tChar);
                    //tTargets.Add(tChar);
            }
        }

        //vProtagonistList
        vTarget = CheckNearest(tTargetList);

    }
    GameObject CheckNearest(List<GameObject> tList)
    {
        //Vector2 tHere = new Vector2(transform.position.x, transform.position.y);
        GameObject tClosestObject = null;
        float tClosestDistance = 0f;
        float tCurrentDistance;
        foreach (GameObject tChar in tList)
        {   if (tClosestObject == null)
            {
                tClosestObject = tChar;
                tClosestDistance = Vector3.Distance(transform.position, tChar.transform.position);
            }
            else
            {
                tCurrentDistance = Vector3.Distance(transform.position, tChar.transform.position);
                if (tCurrentDistance < tClosestDistance)
                {
                    tClosestObject = tChar;
                    tClosestDistance = tCurrentDistance;
                }
            }
            
        }
        return tClosestObject;
    }
    char PointToDirection(Vector2 tHere, Vector3 tThere)
    {
        //return 'a';
        
        char tResult = 'X';
        //Vector3 tGoto;
        //tGoto = cTS.vCurrentTarget.transform.position;
        Vector3 tMyXZ = this.transform.position;
        float tDifferenceX = tHere.x - tThere.x;
        float tDifferenceY = tHere.y - tThere.y;
        float tAngle;
        tAngle = Mathf.Atan2(tDifferenceX, tDifferenceY) * 180 / Mathf.PI;
        if (tAngle < -135)
            tResult = 'E';
        else if (tAngle < -45)
            tResult = 'S';
        else if (tAngle < 45)
            tResult = 'W';
        else if (tAngle < 135)
            tResult = 'N';
        else
            tResult = 'E';
        return tResult;
        
    }

    void GetDirection()
    {


    }
    void CheckPath()
    {

    }

    
}
