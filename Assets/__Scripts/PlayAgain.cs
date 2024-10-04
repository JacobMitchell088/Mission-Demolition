using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    private Button playAgainButton;

    void Start() {
        playAgainButton = GetComponent<Button>();

        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
    }



    void OnPlayAgainClicked() {
        SceneManager.LoadScene("_Scene_0");
    }
}
