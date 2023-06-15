using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TextCarTriggerTutorial : MonoBehaviour
{
    private const string EscapeTutorialText = "I can use this APC to escape!";
    private const string EscapeConditionText = "I need fuel and the keys to get it running!";
    private const string BlankTutorialText = "";
    
    private const string EscapeConditionTextKey = "Im almost out! I just need the gas can!";
    private const string EscapeConditionTextGasCan = "I just need the keys! And Im out";
    
    private const string Escaped = "I have Escaped Alive!";

    public TMP_Text screenText;

    [SerializeField]
    private GameEvents gameEvents;
    
    [SerializeField]
    private GameState gameState;

    private void Awake()
    {
        gameEvents.onItemPickedUp += interactable =>
        {
            if (interactable is GasCan gasCan)
            {
                gameState.hasGasCan = true;

            }
            if (interactable is Key key)
            {
                gameState.hasKey = true;
            }
        };
    }

    IEnumerator CarPartsStart()
    {
        screenText.text = EscapeTutorialText;
        yield return new WaitForSeconds(5f);
        screenText.text = EscapeConditionText;
        yield return new WaitForSeconds(5f);
        screenText.text = BlankTutorialText;
    }
    
    IEnumerator CarStartPartsGasCan()
    {
        screenText.text = EscapeConditionTextGasCan;
        yield return new WaitForSeconds(5f);
        screenText.text = BlankTutorialText;
    }
    
    IEnumerator CarStartPartsKey()
    {
        screenText.text = EscapeConditionTextKey;
        yield return new WaitForSeconds(5f);
        screenText.text = BlankTutorialText;
    }
    
    IEnumerator CarStartPartsEnd()
    {
        screenText.text = Escaped;
        yield return new WaitForSeconds(5f);
        screenText.text = BlankTutorialText;
        yield return new WaitForSeconds(5f);
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gameState.hasKey && !gameState.hasGasCan)
        {
            StartCoroutine(CarPartsStart());
        }
        
        if (collision.gameObject.CompareTag("Player") && gameState.hasKey && !gameState.hasGasCan)
        {
            StartCoroutine(CarStartPartsKey());
        }
        
        if (collision.gameObject.CompareTag("Player") && !gameState.hasKey && gameState.hasGasCan)
        {
            StartCoroutine(CarStartPartsGasCan());
        }
        
        if (collision.gameObject.CompareTag("Player") && gameState.hasKey && gameState.hasGasCan)
        {
            StartCoroutine(CarStartPartsEnd());
        }
    }
}
