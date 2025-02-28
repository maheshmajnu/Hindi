using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    string rootURL = "https://playvroomstudio.com/apps/alphav1/"; //Path where php files are located
    public enum CurrentWindow { Login, Register }
    public CurrentWindow currentWindow = CurrentWindow.Login;

    //Logged-in user data
    public string userName = "";
    public string userEmail = "";
    public string UqID = "";
    public string SessionID = "";
    public string grades = "";


    [SerializeField] private GameObject LoginUI;
    [SerializeField] private GameObject SignupUI;

    [SerializeField] private InputField LoginEmail;
    [SerializeField] private InputField LoginPassword;

    [SerializeField] private InputField SignupEmail;
    [SerializeField] private InputField SignupName;
    [SerializeField] private InputField SignupPhone;
    [SerializeField] private InputField SignupPassword1;
    [SerializeField] private InputField SignupPassword2;

    [SerializeField] private Button LoginBtn;
    [SerializeField] private Button SignupBtn;

    [SerializeField] private Button GotoLoginBtn;
    [SerializeField] private Button GotoSignupBtn;

    bool isWorking = false;
    public bool registrationCompleted = false;
    public bool isLoggedIn = false;


    [SerializeField] private Text errorMessageField;
    string errorMessage = "";

    //get palyer
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _LoginCam;

    //already login check params
    bool isChecking = false;
    private string Uid;
    private string sid;
    private string db_sess_id;

    //update manager params
    public GameObject UpdateCanvas;
    public TextMeshProUGUI UpdateNotes;
    bool isVerChecking = false;

    void Start()
    {



        Uid = StaticVariables.User_Id;
        sid = StaticVariables.Session_Id;

        if (Uid.Length != 0 && sid.Length != 0)
        {
            Debug.Log(Uid + sid);
            if (!isChecking)
            {
                // LoginBtn.interactable = false;
                StartCoroutine(loginCheckSession());
            }
        }
        else
        {
            // dint login yet show updatemanager
            UpdateAvailabilityChecker();
        }
    }


    void UpdateAvailabilityChecker()
    {
        StartCoroutine(UpdateVerCheck());
        // StaticVariables.CurrentApp_Version;
    }

    IEnumerator UpdateVerCheck()
    {
        isVerChecking = true;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("OSbuild", StaticVariables.App_build_Version);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "updatechecker.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    // Update Available
                    string[] verdataChunks = responseText.Split('|');
                    string ver_notes = verdataChunks[1]; ver_notes = ver_notes.Trim();

                    //save and display version notes/update popup
                    Debug.Log(ver_notes);
                    UpdateCanvas.SetActive(true);
                    Debug.Log(ver_notes);
                    UpdateNotes.text = ver_notes;

                }
                else
                {
                    // errorMessage = responseText;  No update available
                    Debug.Log("App UptoDate");
                }
            }
        }

        //errorMessageField.text = errorMessage;
        isVerChecking = false;
    }

    IEnumerator loginCheckSession()
    {
        isChecking = true;
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
                        //stay on page - dont ask login again
                        ActivatePlayer();
                    }
                    else
                    {
                        // Debug.Log("No login or multi logins detected");
                        //logout/ and ask login again
                    }

                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        //errorMessageField.text = errorMessage;
        isChecking = false;
    }

    void OnGUI()
    {

        if (!isLoggedIn)
        {
            Cursor.visible = true;
            if (currentWindow == CurrentWindow.Login)
            {
                SignupUI.SetActive(false);
                LoginUI.SetActive(true);
            }
            if (currentWindow == CurrentWindow.Register)
            {
                LoginUI.SetActive(false);
                SignupUI.SetActive(true);
            }
        }


        //  GUI.Label(new Rect(5, 5, 500, 25), "Status: " + (isLoggedIn ? "Logged-in Username: " + userName + " Email: " + userEmail : "Logged-out"));
        if (isLoggedIn)
        {
            if (GUI.Button(new Rect(5, 30, 100, 25), "Log Out"))
            {
                isLoggedIn = false;
                userName = "";
                userEmail = "";
                currentWindow = CurrentWindow.Login;
            }
        }



        if (errorMessage != "")
        {
            errorMessageField.text = errorMessage;
        }
    }


    public void GotoLoginButtonClicked()
    {
        if (!isWorking)
        {
            //Debug.Log("goto login"); 
            ResetValues();
            errorMessageField.text = errorMessage;
            currentWindow = CurrentWindow.Login;
        }
    }
    public void GotoSignupButtonClicked()
    {
        if (!isWorking)
        {
            //Debug.Log("goto signup"); 
            ResetValues();
            errorMessageField.text = errorMessage;
            currentWindow = CurrentWindow.Register;
        }
    }

    public void OnLoginButtonClicked()
    {
        if (!isWorking)
        {
            // LoginBtn.interactable = false;
            StartCoroutine(LoginEnumerator());
        }
    }


    public void OnSignupButtonClicked()
    {
        if (!isWorking)
        {
            // SignupBtn.interactable = false;
            StartCoroutine(RegisterEnumerator());
        }
    }



    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", LoginEmail.text);
        form.AddField("password", LoginPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
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
                    //dataChunks[0] is "Success";
                    userName = dataChunks[1];
                    userEmail = dataChunks[2];
                    UqID = dataChunks[3];
                    SessionID = dataChunks[4]; SessionID = SessionID.Trim();
                    grades = dataChunks[5];
                    Debug.Log(grades);

                    //store sessionID & uid  in static variable
                    StaticVariables.Session_Id = SessionID;
                    StaticVariables.User_Id = UqID;
                    StaticVariables.Session_Uname = LoginEmail.text;
                    StaticVariables.Session_Psw = LoginPassword.text;
                    StaticVariables.grade_class_name = grades;

                    isLoggedIn = true;
                    ResetValues();

                    // Activate Player
                    ActivatePlayer();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }


        errorMessageField.text = errorMessage;
        isWorking = false;
    }




    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        //registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", SignupEmail.text);
        form.AddField("username", SignupName.text);
        form.AddField("Phone", SignupPhone.text);
        form.AddField("password1", SignupPassword1.text);
        form.AddField("password2", SignupPassword2.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
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
                    registrationCompleted = true;
                    StaticVariables.Session_Uname = SignupEmail.text;
                    StaticVariables.Session_Psw = SignupPassword1.text;
                    ResetValues();
                    currentWindow = CurrentWindow.Login;
                    StartCoroutine(OpenloginURL());
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        errorMessageField.text = errorMessage;
        isWorking = false;
    }


    void ResetValues()
    {
        LoginEmail.text = "";
        LoginPassword.text = "";
        SignupEmail.text = "";
        SignupName.text = "";
        SignupPhone.text = "";
        SignupPassword1.text = "";
        SignupPassword2.text = "";
        errorMessage = "";
    }

    IEnumerator OpenloginURL()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        string nurl = "http://www.playvroomstudio.com/User/login.php?u=" + StaticVariables.Session_Uname + "&p=" + StaticVariables.Session_Psw;
        Application.OpenURL(nurl);
    }




    //Login successful activate player
    void ActivatePlayer()
    {
        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        _Player.SetActive(true);
        _LoginCam.SetActive(false);
        gameObject.SetActive(false);
    }

}
