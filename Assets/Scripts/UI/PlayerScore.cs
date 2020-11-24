using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI leaderNumber = null;
    [SerializeField] TextMeshProUGUI playerName = null;
    [SerializeField] TextMeshProUGUI playerScore = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(int leaderNumber, string newName, int score)
    {
        SetLeaderNumber(leaderNumber);
        SetName(newName);
        SetScore(score);
    }

    public void SetLeaderNumber(int number)
    {
        leaderNumber.text = "#" + number;
    }
    public void SetName(string newName)
    {
        playerName.text = newName;
    }

    public void SetScore(string score)
    {
        playerScore.text = score;
    }
    public void SetScore(int score)
    {
        playerScore.text = "" + score;
    }

}
