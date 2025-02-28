using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class Grade8 : MonoBehaviour
{
     
    private GameObject mainPlayer;

    // Start is called before the first frame update
    private void Start()
    { 
        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoClass()
    {
        //check static variable if this class can be accesible
        string responseText = StaticVariables.grade_class_name;
        string[] accesibleClasses = responseText.Split(',');
        if (accesibleClasses.Contains("8") ) {                                       //CHANGE "->  <-"

            //Call Respective subjects in Chapters-UI 
            mainPlayer.GetComponent<LabRoomManager>().EightClassChapterLab();                    //CHANGE METHOD-NAME()

        }
        else if (responseText.Contains("all"))
        {
            //Call Respective subjects in Chapters-UI 
            mainPlayer.GetComponent<LabRoomManager>().EightClassChapterLab();
        }
        
        else
        {
            //turn Red (unavailable)
            Button Classbutton = gameObject.GetComponent<Button>(); 
            ColorBlock colors = Classbutton.colors;
            colors.normalColor = Color.red;
            colors.selectedColor = Color.red;
            colors.highlightedColor = new Color32(255, 107, 128, 255);
            colors.pressedColor = new Color32(0, 0, 0, 255);
            Classbutton.colors = colors;
        }
          
    }
}
