using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Collections.Generic;

public class testloadaddr : MonoBehaviour
{
    [SerializeField]
    public GameObject Loader; private GameObject panel;
    private bool doneLoading;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI dSizeText;

    //public AssetReference sceneAsset;  
    private bool clearPreviousScene = false;
    private SceneInstance previousLoadedScene;
    private AsyncOperationHandle<SceneInstance> _loadHandle;

    private AsyncOperationHandle<SceneInstance> _loadHandle_Unload;

    private string addressableKey;
    long downloadSize = 0;


    private bool miniWorldScene_OFF = false;


    private void Awake()
    {
        Loader = GameObject.Find("Canvas-Loader-Full");
        panel = Loader.transform.GetChild(0).gameObject;
        loadingSlider = panel.transform.Find("Slider").gameObject.GetComponent<Slider>();
        loadingText = panel.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        dSizeText = panel.transform.Find("Text2").gameObject.GetComponent<TextMeshProUGUI>();

    }


    void Start()
    {

        //Debug.Log(sceneAsset.RuntimeKey);
        //addressableKey = (string)sceneAsset.RuntimeKey;


    }

    //you can directly get addressablekey based on sceneAssetReferecne
    public void btnclick()
    {
        //LoadAddressableLevel(addressableKey);
    }

     



    public void LoadAddressableLevel(string FullAddressableKey)
    {

        string[] verdataChunks = FullAddressableKey.Split('|');
        string addressableKey = verdataChunks[0];
        string ClassKey = verdataChunks[1];


        StaticVariables.scene_addressableKey = FullAddressableKey;

        //start loader
        Loader.SetActive(true);
        Debug.Log("progress bar ON");
        panel.SetActive(true);
        doneLoading = true;
        dSizeText.text = "";

        if (clearPreviousScene)
        {
            Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asyncHandle) =>
            {
                clearPreviousScene = false;
                previousLoadedScene = new SceneInstance();
                Debug.Log("Unloaded scene" + addressableKey + "successfully");
            };
        }


        // Asynchronously retrieve the download size of the asset
        Addressables.GetDownloadSizeAsync(addressableKey).Completed += sizeHandle => {
            downloadSize = sizeHandle.Result;
            Debug.Log($"The download size of the asset is: {downloadSize} bytes");
        };



        _loadHandle = Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Additive);
        _loadHandle.Completed += (asyncHandle) =>
        {

            clearPreviousScene = true;
            previousLoadedScene = asyncHandle.Result;

            StaticVariables.previous_LoadedScene = previousLoadedScene;

            Debug.Log("Loaded scene" + addressableKey + "successfully");
            Debug.Log("progress bar OFF");
            panel.SetActive(false);
            miniWorldScene_OFF = true;

            Debug.Log("ver_addressableKey " + addressableKey);
            Debug.Log("ver_ClassKey " + ClassKey);
        };
    }





    private void UnloadMyScene()
    {
        // Start unloading the scene asynchronously
        var unloadAsync = SceneManager.UnloadSceneAsync("Miniworld");
        // Do something after the scene is unloaded
        Debug.Log("MainGame Scene has been unloaded.");
        // SceneManager.UnloadSceneAsync("Initialize");
        // StartCoroutine(UnloadLevel());
    }

    /*
    private IEnumerator UnloadLevel()
    { 
         
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync("Initialize"); 
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            Debug.Log("Un-Loading progress: " + (asyncOperation.progress * 100) + "%");

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            { 
                // unloaded successfully
            }

            yield return null;
        }
     
    }
    */






    // Update is called once per frame
    void Update()
    {
        if (miniWorldScene_OFF)
        {
            UnloadMyScene();
            miniWorldScene_OFF = false;
        }


        //checking for completion 
        if (_loadHandle.IsValid() && _loadHandle.PercentComplete > 0 && doneLoading)
        {
            // ProgressText.text = "Loading Progress: " + GroundAssetRef.Asset.PercentComplete * 100 + "%"; 
            float progress = _loadHandle.PercentComplete;
            loadingSlider.value = progress;
            loadingText.text = "Downloading Updates : " + (int)Math.Round(progress * 100) + "%";
            Debug.Log(progress * 100 + "%");

            //downloaded size
            float newProgress;
            if (progress <= 0.6f) { newProgress = 0f; } else { newProgress = progress; }
            float dsize = downloadSize / 1048576f;
            float newdsize = ((((newProgress * 100f) - 60f) * dsize) / 100f) * 3f;  //60% - 100%
            if (newdsize > dsize) { newdsize = dsize; }
            dSizeText.text = String.Format("{0:0.00}", newdsize) + " MB/ " + String.Format("{0:0.00}", dsize) + " MB";

            if (_loadHandle.PercentComplete == 1)
            {
                //UnloadMyScene();
                miniWorldScene_OFF = true;
                doneLoading = false;
            }




        }



    }


}
