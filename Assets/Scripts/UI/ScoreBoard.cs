using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score1 = null;
    [SerializeField] TextMeshProUGUI score2 = null;
    [SerializeField] TextMeshProUGUI score3 = null;
    [SerializeField] TextMeshProUGUI score4 = null;
    [SerializeField] TextMeshProUGUI score5 = null;

    // Start is called before the first frame update
    void Start()
    {
        score1.text = "" + PlayerPrefs.GetInt("score1");
        score2.text = "" + PlayerPrefs.GetInt("score2");
        score3.text = "" + PlayerPrefs.GetInt("score3");
        score4.text = "" + PlayerPrefs.GetInt("score4");
        score5.text = "" + PlayerPrefs.GetInt("score5");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
