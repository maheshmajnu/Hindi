using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MultiLoginChecker : MonoBehaviour
{
    private string errorMessage;
    bool isWorking = false;
    string rootURL = "http://playvroomstudio.com/apps/alphav1/"; //Path where php files are located

    private string Uid; 
    private string sid;
    private string db_sess_id;

    // Start is called before the first frame update
    void Start()
    {
        Uid = StaticVariables.User_Id;
        sid = StaticVariables.Session_Id;

        if (!isWorking)
        {
            // LoginBtn.interactable = false;
            StartCoroutine(loginCheckSession());
        } 
    }

    // Update is called once per frame
    IEnumerator loginCheckSession()
    { 
        isWorking = true; 
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("UqID", Uid); 

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "multilogincheck.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    db_sess_id = dataChunks[1]; db_sess_id = db_sess_id.Trim();
                    //Debug.Log(db_sess_id);
                    //check matching
                    if (db_sess_id == sid)
                    {
                        //stay on page
                    } else
                    {
                       // Debug.Log("multi logins detected");
                        //logout/exit
                        Application.Quit();
                    }
                     
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        //errorMessageField.text = errorMessage;
        isWorking = false;
    }
}
