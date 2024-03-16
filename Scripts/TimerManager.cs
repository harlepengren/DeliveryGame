using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float timerLength;
    [SerializeField] GameObject clockText;

    private TextMeshProUGUI timerObject;
    private float currentTime;
    private Action timerAction;
    private bool timerEnabled;

    void Start(){
        timerEnabled = false;    
    }

    private void DisplayTime(bool endTime=false){
        if(!endTime){
            string strSeconds;

            float remaining = timerLength - currentTime;
            int minutes = (int)Math.Floor(remaining/60.0f);
            int seconds = (int)Math.Floor(remaining - (minutes*60));
            
            if(seconds < 10){
                strSeconds = "0" + seconds.ToString();
            } else {
                strSeconds = seconds.ToString();
            }

            timerObject.text = minutes+":"+strSeconds;
        } else {
            timerObject.text = "0:00";
        }
    }
    public void StartTimer(Action callback){
        timerObject = clockText.GetComponent<TextMeshProUGUI>();
        timerAction = callback;
        currentTime = 0.0f;
        timerEnabled = true;

        if(timerObject != null){
            DisplayTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerEnabled){
            currentTime += Time.deltaTime;
            DisplayTime();

            if(currentTime >= timerLength){
                timerAction();
                timerEnabled = false;
                DisplayTime(true);
            }
        }
    }
}
