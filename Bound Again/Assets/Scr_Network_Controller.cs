using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scr_Network_Controller : NetworkBehaviour
{
    public string test;
    [SyncVar]
    public sSendMessage vSendMessage;
    public sReceiveMessage vReceiveMessage;

    [System.Serializable]
    public class sSendMessage
    {
        public bool Sending;
        public int vStep;
        public string vCharacter;
        public string vAction;
    }
    //[System.Serializable]
    public class sReceiveMessage
    {
        public int vStep;
        public string vCharacter;
        public string vAction;
    }
    [Command]
    public void CmdUpdateNetwork(float tFloat)
    {
        //vValue = tFloat;
    }
}
