using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Protagonist : MonoBehaviour
{
    static float vMultiplyTime = 4f;

    public string vCharacter; // Warrior, Mage, Guard, Theif
    public string vAction = "None"; // Action given when controller is used [] Not coverted to skills used by character;
    public string vActionType;
    public float vAnimationFrame;
    public bool vIsAnimating;
    public Vector3 vStartingPosition;
    public Vector3 vDestinationPosition;
    public AnimationCurve vYCurve;
    public Scr_DungeonEngine_Controller cDEC;
    public LayerMask vObstacleLayer;

    public string vMovementUp;
    public string vMovementDown;
    public string vMovementLeft;
    public string vMovementRight;
    public string vButtonA;
    public string vButtonB;
    public string vButtonX;
    public string vButtonY;
    public string vButtonBumper;
    public string vButtonTrigger;
    public string vButtonMenu;

    public Vector3 vPreviousLandedSpot;
    [System.Serializable]
    public class BuffsDebuffs
    {
        public string vBuffName = "N/A";
        public int vTurnsToExpire = 0;

    }
    // Use this for initialization
    void Start ()
    {
        Scr_Tile_Animation tTile_Animation;
        Ray tRay = new Ray(transform.position, Vector3.down);
        RaycastHit tHit;
        if (Physics.Raycast(tRay, out tHit))
        {
            Debug.Log("HIT");
            if (tHit.collider.tag == "Block")
            {
                tTile_Animation = tHit.collider.GetComponent<Scr_Tile_Animation>();
                tTile_Animation.vAttachedObject = this.gameObject;
                
                //Vector3 tVect3 = tTile_Animation.transform.position;
                //tVect3.y += tTile_Animation.vYCurrent;
                tTile_Animation.vAttachedYCurrent = tTile_Animation.vYCurrent;
                transform.position = tTile_Animation.transform.position;
            }
        }
        fSetAction();

    }
	
	// Update is called once per frame
	void Update () {
        if (!vIsAnimating)
            return;
        fAnimate();
    }
    void fSetAction() // 
    {
        vMovementUp = "MoveUp";
        vMovementDown = "MoveDown";
        vMovementLeft = "MoveLeft";
        vMovementRight = "MoveRight";
        vButtonA = "SkillA";
        vButtonB = "SkillB";
        vButtonX = "SkillC";
        vButtonY = "SkillD";
        vButtonBumper = "ItemMaybe";
        vButtonTrigger = "Vent";
        vButtonMenu = "Menu";
    }
    void fSetConfused()
    {
        vMovementUp = "MoveLeft";
        vMovementDown = "SkillA";
        vMovementLeft = "Vent";
        vMovementRight = "SkillD";
        vButtonA = "ItemMaybe";
        vButtonB = "MoveUp";
        vButtonX = "MoveRight";
        vButtonY = "SkillB";
        vButtonBumper = "MoveDown";
        vButtonTrigger = "SkillC";
        vButtonMenu = "Menu";
    }
    
    public void fSetAnimation()
    {
        vStartingPosition = transform.position;
        vDestinationPosition = vStartingPosition;
        vAction = ActionCheck(vAction);

        switch (vAction)
        {
            case "MoveUp": vActionType = "Move"; vDestinationPosition.z += 1; break;
            case "MoveDown": vActionType = "Move"; vDestinationPosition.z -= 1; break;
            case "MoveLeft": vActionType = "Move"; vDestinationPosition.x -= 1; break;
            case "MoveRight": vActionType = "Move"; vDestinationPosition.x += 1; break;
            default: vActionType = "Vent"; break;
                /*
            case "ActionA": vActionType = "Vent"; break;
            case "ActionB": vActionType = "Vent"; break;
            case "ActionX": vActionType = "Vent"; break;
            case "ActionY": vActionType = "Vent"; break;
            case "ActionBumper": vActionType = "Vent"; break;
            case "ActionTrigger": vActionType = "Vent"; break;
            case "ActionMenu": vActionType = "Vent"; break;
            */
        }
        vAnimationFrame = 0f;
        vIsAnimating = true;
        vDestinationPosition = Vect3Round(vDestinationPosition);
    }
    Vector3 Vect3Round(Vector3 tConvert)
    {
        tConvert = new Vector3(Mathf.RoundToInt(tConvert.x), Mathf.RoundToInt(tConvert.y), Mathf.RoundToInt(tConvert.z));
        return tConvert;
    }
    public void fAnimate()
    {
        Vector3 tPosition;

        vAnimationFrame += Time.deltaTime * vMultiplyTime;
        if (vAnimationFrame >= 1f)
        {
            vIsAnimating = false;
        }
        vAnimationFrame = Mathf.Clamp(vAnimationFrame, 0f, 1f);
        tPosition = Vector3.Lerp(vStartingPosition, vDestinationPosition, vAnimationFrame);
        tPosition.y += vYCurve.Evaluate(vAnimationFrame);
        transform.position = tPosition;

    }
    public string ActionCheck(string tAction)
    { string Result = "None";
        switch (tAction)
        {
            case "MoveUp": Result = vMovementUp; break;
            case "MoveDown": Result = vMovementDown; break;
            case "MoveLeft": Result = vMovementLeft; break;
            case "MoveRight": Result = vMovementRight; break;
            case "ActionA": Result = vButtonA; break;
            case "ActionB": Result = vButtonB; break;
            case "ActionX": Result = vButtonX; break;
            case "ActionY": Result = vButtonY; break;
            case "ActionBumper": Result = vButtonBumper; break;
            case "ActionTrigger": Result = vButtonTrigger; break;
            case "ActionMenu": Result = vButtonMenu; break;




        }
        return Result;

    }
    void CheckDestination(Vector3 tHere, Vector3 tThere)
    {
        bool tFinished = false;
        float tDistance = Vector3.Distance(tHere, tThere);
        int tCount = 0;
        string tResult = "None";
        Vector3 vPreviousLocation = fRoundVector3(transform.position);

        while (!tFinished) {
            tCount++;
            Ray tRay = new Ray(tHere, (tThere- tHere).normalized);
            RaycastHit tHit;
            if (Physics.Raycast(tRay, out tHit, tCount, vObstacleLayer))
            {
                string tTag = tHit.collider.tag;
                switch (tTag)
                {
                    case "Protagonist":
                        tResult = "Bump";
                        break;
                    case "Antagonist":
                        tResult = "Attack";
                        break;
                    case "Obstacle":
                        tResult = "Attack";
                        break;
                }
                vPreviousLocation = vPreviousLocation + (tRay.direction * tCount);
                tFinished = true;

            }
            else
            {

                tFinished = true;
            }

            //vDestinationPosition
        }


    }
    Vector3 fRoundVector3(Vector3 tVector3)
    {
        tVector3.x = Mathf.RoundToInt(tVector3.x);
        tVector3.y = Mathf.RoundToInt(tVector3.y);
        tVector3.z = Mathf.RoundToInt(tVector3.z);
        return tVector3;
    }
    void CheckFloor(Vector3 tHere)
    {
        Ray tRay = new Ray(transform.position, Vector3.down);
        RaycastHit tHit;
        if (Physics.Raycast(tRay, out tHit))
        {
            //Debug.Log("HIT");
            //if (tHit.collider.tag == "Block")
            //{
            //}/
        }
    }
    
}
