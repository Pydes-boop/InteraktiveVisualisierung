﻿//script by Philipp
//script to animate the tiele screen text

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class TitleCardTextAnimation : MonoBehaviour
{

    private string CurrentText = "";

    public string pathToText = "Assets/TextMaterial/TitleCardText.txt";

    public Font textFont;

    public Text textField;
    public Button nextLine;
    public string nextScene = "";

    private int lineCount = 0;
    public int maxLines = 2;
    public float charspeed = 0.1f;

    private float timePassed = 0;
    private int textPointer = 0;

    private List<string> lines;

    private bool hold = false;

    private Vector3 buttonHide;
    private Vector3 buttonShow;

    private void Awake()
    {
        this.textField.text = "";

        this.lines = File.ReadLines(pathToText).ToList();

        this.buttonHide = nextLine.transform.position;
        this.buttonHide.y = 999999;
        this.buttonShow = nextLine.transform.position;
        this.nextLine.transform.position = buttonHide;

        this.nextLine.GetComponentInChildren<Text>().font = textFont;
        this.textField.font = textFont;
    }

    void Update()
    {
        if (hold) return;
        
        if (!textHasNext(CurrentText))
        {
            if (!hasNextLine())
            {
                this.nextLine.onClick.RemoveAllListeners();
                this.nextLine.onClick.AddListener(switchScene);
                this.nextLine.transform.position = buttonShow;
                hold = true;
                return;
            }

            if (lineCount > 0 && lineCount % maxLines == 0)
            {
                this.nextLine.onClick.RemoveAllListeners();
                this.nextLine.onClick.AddListener(stepOver);
                this.nextLine.transform.position = buttonShow;
                hold = true;
                return;
            }

            CurrentText = getNextLine();
            if (lineCount > 0) textField.text += "\n";
            timePassed = 0;
            textPointer = 0;
            lineCount++;
            return;
        }

        if (timePassed > charspeed && textHasNext(CurrentText)) 
        {
            textField.text += CurrentText.ToCharArray()[textPointer];
            timePassed = 0;
            textPointer++;
        }

        timePassed += Time.deltaTime;
    }

    public void stepOver() 
    {
        this.hold = false;
        CurrentText = getNextLine();
        textField.text += "\n";
        timePassed = 0;
        textPointer = 0;
        textField.text = "";
        lineCount++;
        this.nextLine.onClick.RemoveAllListeners();
        this.nextLine.transform.position = buttonHide;
    }

    public void switchScene() 
    {

        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        this.nextLine.transform.position = buttonHide;

    }

    private bool textHasNext(string text) 
    {
        return text.Length > textPointer;
    }

    private bool hasNextLine() 
    {
        return this.lines != null && this.lineCount < this.lines.Count;
    }

    private string getNextLine() 
    {
        if (this.lines == null || this.lineCount >= this.lines.Count) return "null";
        return this.lines.ElementAt(this.lineCount);
    }
}
