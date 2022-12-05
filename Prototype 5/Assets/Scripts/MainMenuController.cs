using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "H I G H S C O R E: " + PlayerPrefs.GetInt("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
