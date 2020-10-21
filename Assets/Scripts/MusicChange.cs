using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChange : MonoBehaviour
{
    [SerializeField]
    AudioPlay audioPlay;
    [SerializeField]
    Scene scene;
    [SerializeField]
    string namescene;
    public string Theme1;
    public string Theme2;
    public string Theme3;
    public bool isplaying1;
    public bool isplaying2;
    public bool isplaying3;
    public string namemusic;

    private void Start()
    {
        //namemusic = Theme1;
        audioPlay = GetComponentInChildren<AudioPlay>();
       
        

    }

    private void Update()
    {
        
        namescene = SceneManager.GetActiveScene().name;
        if (namescene == "MainMenu"&& !isplaying1)
        {
            namemusic = Theme1;
            isplaying1 = true;
            isplaying2 = false;
            isplaying3 = false;
            audioPlay.PlayAudio(namemusic);
        }
        if (namescene == "Scene1"&&!isplaying2)
        {
            namemusic = Theme2;
            isplaying2 = true;
            isplaying1 = false;
            isplaying3 = false;
            audioPlay.PlayAudio(namemusic);
        }
        if (namescene == "Scene2"&& !isplaying3)
        {
            namemusic = Theme3;
            isplaying3 = true;
            isplaying1 = false;
            isplaying2 = false;
            audioPlay.PlayAudio(namemusic);
        }
      

    }

}
