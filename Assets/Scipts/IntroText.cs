using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroText : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] float delayFade;
    [SerializeField] float fadeSpeed;
    TextMeshProUGUI text;
    void Start()
    {
        text=GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeText(delayFade,fadeSpeed));
    }

    IEnumerator FadeText(float delay,float speed){
        yield return new WaitForSeconds(delay); 
        float alpha = 100f;
        while(alpha>0){
            alpha -= speed * Time.deltaTime;
            text.color = new Color(text.color.r,text.color.g,text.color.b,alpha/100f);
            //Debug.Log((alpha,alpha/100f));
            yield return null;
        }
    }
}