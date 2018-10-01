using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Scr_DungeonEngine_Controller : MonoBehaviour {

    public bool vIsNetworking;
    public Scr_Network_Controller cNC;
    public ControlSystemPerPlayer[] vCharlist;
    public int vCount;
    public float gAnimationFrame;
    public GameObject vWarrior;
    public GameObject vMage;

    public Vector2 vTileAnimationSpot;

    public float vMaxAnimationTime = 1f;
    public Scr_Input_Xbox[] vXboxInputSources;
    
    public eTurnState vCurrentState = eTurnState.ProtagonistInputWait; // Tried using enum. You might as well use string cause on switch case, the case accepts only int
    public enum eTurnState { ProtagonistInputWait ,
        ProtagonistStartAnimate , ProtagonistAnimation , ProtagonistEndAnimate  ,
        AntagonistStart , AntagonistAnimate , AntagonistEndAnimate ,
        EnvironmentStart , EnvironmentAnimate , EnvironmentEnd ,
        SubEventStart , SubEventAnimate , SubEventEnd // Possible Scene Events
        
    }
    /* State List for current State
        ProtagonistInputWait 
        ProtagonistStartAnimate -> ProtagonistAnimation     -> ProtagonistEndAnimate
        AntagonistStart         -> AntagonistAnimate        -> AntagonistEndAnimate
        EnvironmentStart        -> EnvironmentAnimate       -> EnvironmentEnd
        SubEventStart           -> SubEventAnimate          -> SubEventEnd
    */
    private GameObject[] vBlockList;
    public int vBlockCount;
    [System.Serializable]
    public class ControlSystemPerPlayer
    {
        public string vPlayer = "Local"; // Local, Network
        public string vCharacter = "None"; // Warrior, Mage
        public GameObject vObjectControlled = null; // GameObject being controlled
        public string vPlayerInputSource = "Controller0"; // Input Source
        public string vExtraStatus = "None";
        public Scr_Protagonist cProtaginst;
    }
    // Use this for initialization
    void Start() {
        vTileAnimationSpot.x = transform.position.x;
        vTileAnimationSpot.y = transform.position.z;
        vCharlist = new ControlSystemPerPlayer[2];
        Scr_Input_Xbox cIX;
        ControlSystemPerPlayer tCSPP = new ControlSystemPerPlayer();

        //\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ Warrior Init Setup //\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        { 
        tCSPP.vPlayer = "Local";
        tCSPP.vPlayerInputSource = "XBox";
        tCSPP.vExtraStatus = "None";
        tCSPP.vCharacter = "Warrior";
        tCSPP.vObjectControlled = Instantiate(vWarrior);
        tCSPP.vObjectControlled.transform.position = new Vector3(-1, 1, 0);
        tCSPP.cProtaginst = tCSPP.vObjectControlled.GetComponent<Scr_Protagonist>();
        /////////// XBOX INPUT SETUP /////////////////////////// Separate when more inputs are available including networking
        cIX = this.gameObject.AddComponent<Scr_Input_Xbox>();
        cIX.vCharacterControlled = "Warrior";
        cIX.vIsActive = true;
        cIX.vIsLeft = true;
        cIX.vOrderedUnit = tCSPP.cProtaginst;
        }
        ////////////////////////////////////////////////////////
        vCharlist[0] = tCSPP;
        //\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ Mage Init Setup //\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        {
            tCSPP = new ControlSystemPerPlayer();
            tCSPP.vPlayer = "Local";
            tCSPP.vPlayerInputSource = "XBox";
            tCSPP.vExtraStatus = "None";
            tCSPP.vCharacter = "Mage";
            tCSPP.vObjectControlled = Instantiate(vMage);
            tCSPP.vObjectControlled.transform.position = new Vector3(1, 1, 0);
            tCSPP.cProtaginst = tCSPP.vObjectControlled.GetComponent<Scr_Protagonist>();

            /////////// XBOX INPUT SETUP /////////////////////////// Separate when more inputs are available including networking
            cIX = this.gameObject.AddComponent<Scr_Input_Xbox>();
            cIX.vCharacterControlled = "Mage";
            cIX.vIsActive = true;
            cIX.vOrderedUnit = tCSPP.cProtaginst;
        }
        ////////////////////////////////////////////////////////
        vCharlist[1] = tCSPP;

    }
	
	// Update is called once per frame
	void Update () {
        //bool vContinue;
        int tCount = 0;
        vCount = 0;
		switch (vCurrentState)
        {
            case eTurnState.ProtagonistInputWait: fProtagonistInputWait(); break;
            case eTurnState.ProtagonistStartAnimate: fProtagonistStartAnimate();
                Debug.Log("StartAnimate");
                // Conflict Check (

                break;
            case eTurnState.ProtagonistAnimation:
                //Debug.Log("Animate");
                foreach (ControlSystemPerPlayer tChar in vCharlist)
                {
                    if (tChar.cProtaginst.vIsAnimating)
                        tCount++;
                }
                if (tCount == 0)
                    vCurrentState = eTurnState.ProtagonistEndAnimate;
                break;
            case eTurnState.ProtagonistEndAnimate:
                Debug.Log("EndAnimate");

                vCurrentState = eTurnState.ProtagonistInputWait;
                break;
            case eTurnState.SubEventStart:
                //vBlockList = GameObject.FindGameObjectsWithTag("Block");

                vCurrentState = eTurnState.SubEventAnimate;
                break;
            case eTurnState.SubEventAnimate:
                if (vBlockCount <= 0)
                    vCurrentState = eTurnState.ProtagonistInputWait;
                break;



        }
	}
    void fProtagonistInputWait()
    {
        vCount = 0;
        foreach (ControlSystemPerPlayer tChar in vCharlist)
        {
            if (tChar.cProtaginst.vAction == "None")
                //if (!tChar.cProtaginst.vIsAnimating)
                vCount++;
        }
        if (vCount == 0)
        {
            fProtagonistStartAnimate();
            vCurrentState = eTurnState.ProtagonistStartAnimate;
            // gAnimationFrame = 0f;
        }

    }
    void fProtagonistStartAnimate()
    {
        //gAnimationFrame += Time.deltaTime;
        Mathf.Clamp(gAnimationFrame, 0f, vMaxAnimationTime);
        foreach (ControlSystemPerPlayer tChar in vCharlist)
        {
            tChar.cProtaginst.fSetAnimation();
        }
        vCurrentState = eTurnState.ProtagonistAnimation;
    }
}
