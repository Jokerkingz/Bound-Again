using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scr_CharacterPiece_Player : MonoBehaviour {
    public string vControlType = "Player"; // Player reuires input, NPC Does not

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

    [System.Serializable]
    public class BuffsDebuffs
    {
        public string vBuffName = "N/A";
        public int vTurnsToExpire = 0;

    }
	// Use this for initialization
	void Start () {
		
	}
    //public void CMD
	void fSetAction()
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

    // Update is called once per frame
    void Update () {
		
	}
}
