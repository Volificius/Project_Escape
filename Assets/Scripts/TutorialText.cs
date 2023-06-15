using System;
using  System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private const string movementTutorialText = "Use W A S D to move the Player!";
    private const string rotationTutorialText = "Use The Mouse to rotate the Player!";
    private const string sprintTutorialText = "Hold SHIFT while moving to sprint!";
    private const string shootTutorialText = "Press Left Click to shoot!";
    private const string endTutorialText = "Avoid getting shot and pickup health along the way if you need it!";
    private const string blankTutorialText = "";


    public TMP_Text screenText;

    public GameObject carTrigger1;
    private void Awake()
    {
        StartCoroutine(TutorialTextCoroutine());
    }

    IEnumerator TutorialTextCoroutine()
    {
        screenText.text = movementTutorialText;
        yield return new WaitForSeconds(5f);
        screenText.text = rotationTutorialText;
        yield return new WaitForSeconds(5f);
        screenText.text = sprintTutorialText;
        yield return new WaitForSeconds(5f);
        screenText.text = shootTutorialText;
        yield return new WaitForSeconds(5f);
        screenText.text = endTutorialText;
        yield return new WaitForSeconds(5f);
        screenText.text = blankTutorialText;

    }

    void Update()
    {
        
    }
    
}
