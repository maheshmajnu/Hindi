using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class AdditiveSceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject Loader; private GameObject panel;
    private GameObject Loader_Full; private GameObject panel_full;
    [SerializeField]
    private GameObject disclaimerPanel;
    [SerializeField] 
    private Slider loadingSlider;
    [SerializeField] 
    private TextMeshProUGUI loadingText;
    

    public String SceneName;


    private void Awake()
    {
        Loader = GameObject.Find("Canvas-Loader-Bar");
        panel = Loader.transform.GetChild(0).gameObject;
        loadingSlider = panel.transform.Find("Slider").gameObject.GetComponent<Slider>();
        loadingText = panel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();

        Loader_Full = GameObject.Find("Canvas-Loader-Full");
        panel_full = Loader_Full.transform.GetChild(0).gameObject;

        disclaimerPanel = GameObject.Find("Disclaimer Pop up");
        
    }

    void Start()
    {
        panel_full.SetActive(false);
        disclaimerPanel.SetActive(true);

    }

    public void OnDisclaimerAccepted()
    {
        // Hide disclaimer panel and show loader
        disclaimerPanel.SetActive(false);
        panel.SetActive(true);

        // Start loading the scene
        StartCoroutine(LoadSceneAsync());
    }

    public void OnDisclaimerDeclined()
    {
        // Exit application or loop back to disclaimer
        Debug.Log("Player declined the disclaimer. Exiting application.");
        Application.Quit();
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            float progress = asyncLoad.progress;
            loadingSlider.value = progress; 
            loadingText.text = "Initiating : " + (int)Math.Round(progress * 100) + "%";
            Debug.Log(progress * 100);
            yield return null;
        }

        if (asyncLoad.isDone)
        {
            //hide loader
            panel.SetActive(false);

            Scene newScene = SceneManager.GetSceneByName(SceneName);
            SceneManager.SetActiveScene(newScene);
        }
    }
}
