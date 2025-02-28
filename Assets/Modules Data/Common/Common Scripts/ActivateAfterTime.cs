using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//========================COMMON SCRIPT=============================
//========== Use this to sequencetially enable Gameobjects ============
//==========-------Works only for single gameobject-------==========

public class ActivateAfterTime : MonoBehaviour
{
    public GameObject TurnOnThisGameObjectAfterTime;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnOnAfterTime());
    }


    IEnumerator TurnOnAfterTime()
    {
        //play your sound
        yield return new WaitForSeconds(time); //waits time T seconds
        TurnOnThisGameObjectAfterTime.SetActive(true);
    }
 
}
