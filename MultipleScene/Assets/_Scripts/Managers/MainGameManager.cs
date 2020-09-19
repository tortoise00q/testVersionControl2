using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager instance;

    [Header("Game Settings")]
    [SerializeField] float timeLimit = 180;

    [Header("UI Settings")]
    [SerializeField] Text score_text;
    [SerializeField] Text time_text;
    [SerializeField] GameObject endGamePanel;

    float current_timeLimit;
    int score;

    public bool playing { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        current_timeLimit = timeLimit;
        time_text.text = TimeConversion.SecondsConverToText(current_timeLimit);
        score_text.text = score + "";
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing && current_timeLimit > 0)
        {
            current_timeLimit -= Time.deltaTime;
            time_text.text = TimeConversion.SecondsConverToText(current_timeLimit);
        }
        else if(!endGamePanel.activeSelf)
        {
            playing = false;
            endGamePanel.SetActive(true);
        }
    }

    public void CollectedBall()
    {
        if (playing)
        {
            score++;
            score_text.text = score + "";
        }
    }
}
