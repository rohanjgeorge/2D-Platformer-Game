using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void RefreshUI(int hearts)
    {
        scoreText.text = "Score: " + hearts;
    }
}