using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class PersistentSceneLoader : MonoBehaviour
{ 
    [SerializeField]
    public GameObject Loader; private GameObject panel;
    private bool doneLoading;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    public GameObject loaderUI;
    public Slider progressSlider;

    private AsyncOperationHandle<SceneInstance> _loadHandle;

    private void Awake()
    {
        Loader = GameObject.Find("Canvas-Loader-Full");
        panel = Loader.transform.GetChild(0).gameObject;
        loadingSlider = panel.transform.Find("Slider").gameObject.GetComponent<Slider>();
        loadingText = panel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        string FullAddressableKey = StaticVariables.scene_addressableKey;

        string[] verdataChunks = FullAddressableKey.Split('|');
        string addressableKey = verdataChunks[0];
        string ClassKey = verdataChunks[1];

        LoadPersistentScene(addressableKey, ClassKey); 
    }

    /*

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutines(index));
    } 
    public IEnumerator LoadScene_Coroutines(int index)
    {
        progressSlider.value = 0;
        loaderUI.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if (progress >= 0.9f)
            {
                progressSlider.value = 0.96f;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;

        }

    }
    */

    void LoadPersistentScene(string addressableKey, string _classKey )
    { 
        Debug.Log("progress bar ON");
        panel.SetActive(true);
        doneLoading = true;

        _loadHandle = Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Additive);
        _loadHandle.Completed += (asyncHandle) =>
        {
            Debug.Log("Loaded scene" + addressableKey + "successfully");
            Debug.Log("progress bar OFF");
            panel.SetActive(false);

            //switch respective _Main for ClassKey
            if (_classKey != null)
            {
                GameObject.Find("ADSceneClassSwitch").GetComponent<ADSceneClassSwitch>().ADSceneClassSwitcher(_classKey);
            }
        };

    }

    void Update()
        {
            //checking for completion 
            if (_loadHandle.IsValid() && _loadHandle.PercentComplete > 0 && doneLoading)
            {
                // ProgressText.text = "Loading Progress: " + GroundAssetRef.Asset.PercentComplete * 100 + "%"; 
                float progress = _loadHandle.PercentComplete;
                loadingSlider.value = progress; 
                loadingText.text = "Downloading Updates : " + (int)Math.Round(progress * 100) + "%";
                Debug.Log("persistent progress"+ progress * 100 + "%");

                if (_loadHandle.PercentComplete == 1)
                { 
                    doneLoading = false;
                }
            }

             
        }

}
