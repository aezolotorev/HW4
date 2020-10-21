using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectionController : MonoBehaviour
{



    public void StartLevel1()
    {
        LoadingScreen.instance.LoadScene("Scene1");
    }
    public void StartLevel2()
    {
        
        LoadingScreen.instance.LoadScene("Scene2");
        
    }

}
