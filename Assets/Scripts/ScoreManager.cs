using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using System.Linq;
public class ScoreManager : MonoBehaviour

{

    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;
    string score;

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore() {
        score =  string.Join("", inputScore.text.ToCharArray().Where(Char.IsDigit));
        submitScoreEvent.Invoke(inputName.text, int.Parse(score));
    }
}
