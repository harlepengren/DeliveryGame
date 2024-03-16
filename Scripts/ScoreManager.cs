using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject scoreObject;
    private TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "$" + score.ToString();
    }

    public void AddScore(int amount){
        score += amount;
    }

    public void DecreaseScore(int amount){
        score -= amount;
    }

    public int GetScore(){
        return score;
    }
}
