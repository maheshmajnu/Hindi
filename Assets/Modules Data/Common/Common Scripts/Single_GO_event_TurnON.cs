using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==============COMMON SCRIPT===================
//==========For Animation Key EVENTS============
//-------Works only for single gameobject-------
//==============================================

//How to Use: 
//1.Apply this on gameobj "A" that has animation
//2. add any desired gameobject "B" via inspector which u want to enable
//3. create animation EVENT and assign _TurnonThisGameObject()


public class Single_GO_event_TurnON : MonoBehaviour
{
    public GameObject TurnOnThisGameObjectFromEvent;
     

    void _TurnonThisGameObject()
    {
        TurnOnThisGameObjectFromEvent.SetActive(true);
    }
}
