using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;
    private float elapsedTime = 4f;
    private float desiredDuration = 3f;

    [SerializeField] AnimationCurve curve;

    private float fillAmount = 1;

    public void SetHealth(int health, int maxHealth, bool instant = false) 
    {
        elapsedTime = 0;

        fillAmount = (float) health / maxHealth;

        if (instant) 
        {
            image.fillAmount = fillAmount;
        }
    }

    private void Update()
    {
        if (elapsedTime < desiredDuration)
        {

            elapsedTime += Time.deltaTime;
            float precentageComplete = elapsedTime / desiredDuration;


            image.fillAmount = Mathf.Lerp(image.fillAmount, fillAmount, curve.Evaluate(precentageComplete));
        }
    }
}
