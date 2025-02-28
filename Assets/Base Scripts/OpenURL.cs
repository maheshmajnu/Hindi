using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url;
    public string Module_URL;
    private string Language;

    private void Awake()
    {
        Language = StaticVariables.Session_Lang;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Open_url()
    {
        Application.OpenURL(url);
    } 

    public void Open_DigitalNotes()
    {
        if (Language == "english")
        {
            //Module_URL = Module_URL;
        }
        else if (Language == "hindi")
        {
            Module_URL = Module_URL + "_hn";
        }
        string nurl = url +"?u="+ StaticVariables.Session_Uname + "&p="+ StaticVariables.Session_Psw+"&go=digitalNotes&m="+Module_URL ;
        Application.OpenURL(nurl);
    }

    public void LogintoDashboard()
    {
        string nurl = url + "?u=" + StaticVariables.Session_Uname + "&p=" + StaticVariables.Session_Psw + "&go=dashboard";
        Application.OpenURL(nurl);
    }
}
