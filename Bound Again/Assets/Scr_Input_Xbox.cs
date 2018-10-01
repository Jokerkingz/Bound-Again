using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Input_Xbox : MonoBehaviour {
    public bool vIsActive;
    public bool vIsLeft;
    public string vAction = "None";

    public string vCharacterControlled; // Warrior Mage
    public Scr_Protagonist vOrderedUnit;

    public float vHoldCheck;
    public float vMaxHold = 0f; // How long does it take for actions to register
    public string vPreviousAction;
    //public Scr_Global cG;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        vAction = "None";
        if (vIsActive)
        {
            if (vIsLeft)
            {
                if (Input.GetAxis("Left_AnalogY") * Mathf.Sign(Input.GetAxis("Left_AnalogY")) > Input.GetAxis("Left_AnalogX") * Mathf.Sign(Input.GetAxis("Left_AnalogX")))
                {
                    if (Input.GetAxisRaw("Left_AnalogY") < 0f) vAction = "MoveUp";
                    if (Input.GetAxisRaw("Left_AnalogY") > 0f) vAction = "MoveDown";
                }
                else
                {
                    if (Input.GetAxisRaw("Left_AnalogX") < 0f) vAction = "MoveLeft";
                    if (Input.GetAxisRaw("Left_AnalogX") > 0f) vAction = "MoveRight";
                }
                if (Input.GetAxis("Left_DPadY") * Mathf.Sign(Input.GetAxis("Left_DPadY")) > Input.GetAxis("Left_DPadX") * Mathf.Sign(Input.GetAxis("Left_DPadX")))
                {
                    if (Input.GetAxisRaw("Left_DPadY") < 0f) vAction = "ActionA";
                    if (Input.GetAxisRaw("Left_DPadY") > 0f) vAction = "ActionY";
                }
                else
                {
                    if (Input.GetAxisRaw("Left_DPadX") < 0f) vAction = "ActionX";
                    if (Input.GetAxisRaw("Left_DPadX") > 0f) vAction = "ActionB";
                }
                if (Input.GetButton("Left_Bumper")) vAction = "ActionBumper";
                if (Input.GetAxis("LR_Trigger") > 0f) vAction = "ActionTrigger";
                if (Input.GetButton("Left_Select")) vAction = "ActionMenu";
                if (Input.GetButton("Left_AnalogPress")) vAction = "ActionPress";
            }
            else
            {
                if (Input.GetAxis("Right_AnalogY") * Mathf.Sign(Input.GetAxis("Right_AnalogY")) > Input.GetAxis("Right_AnalogX") * Mathf.Sign(Input.GetAxis("Right_AnalogX")))
                {
                    if (Input.GetAxisRaw("Right_AnalogY") < 0f) vAction = "MoveUp";
                    if (Input.GetAxisRaw("Right_AnalogY") > 0f) vAction = "MoveDown";
                }
                else
                {
                    if (Input.GetAxisRaw("Right_AnalogX") < 0f) vAction = "MoveLeft";
                    if (Input.GetAxisRaw("Right_AnalogX") > 0f) vAction = "MoveRight";
                }
                if (Input.GetButton("Right_ButtonA")) vAction = "ActionA";
                if (Input.GetButton("Right_ButtonB")) vAction = "ActionB";
                if (Input.GetButton("Right_ButtonX")) vAction = "ActionX";
                if (Input.GetButton("Right_ButtonY")) vAction = "ActionY";
                if (Input.GetButton("Right_Bumper")) vAction = "ActionBumper";
                if (Input.GetAxis("LR_Trigger") < 0f) vAction = "ActionTrigger";
                if (Input.GetButton("Right_Start")) vAction = "ActionMenu";
                if (Input.GetButton("Right_AnalogPress")) vAction = "ActionPress";

            }
        }
        if (vAction == vPreviousAction)
        {
            vHoldCheck += Time.deltaTime;
            if (vHoldCheck >= vMaxHold)
            {
                vOrderedUnit.vAction = vAction;
            }
        }
        else
        {
            vHoldCheck = 0;
            vOrderedUnit.vAction = "None";
        }


        vPreviousAction = vAction;
    }
}
