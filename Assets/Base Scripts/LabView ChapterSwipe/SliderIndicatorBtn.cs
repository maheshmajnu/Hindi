using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderIndicatorBtn : MonoBehaviour
{
    ChapterSwipe chapSwip;
    private GameObject content;
    [SerializeField]
    private Button _startButton;

    // Start is called before the first frame update
    void Start()
    {
        //select script for ChapterSlideContent gameObject
        content = GameObject.Find("ChapterSlideContent"); 
        chapSwip = content.GetComponent<ChapterSwipe>();

        //Onclick for this gameObject's Button
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        //call function that is located in different script (ChapterSlideContent -> ChapterSwipe.cs)
        chapSwip.WhichBtnClicked(_startButton);
    }
}
