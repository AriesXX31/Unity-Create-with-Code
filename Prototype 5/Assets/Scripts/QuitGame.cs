using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Quitgame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quitgame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log(button.gameObject.name + " was clicked");
    }
}
