using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcController : MonoBehaviour
{
    [SerializeField] bool increaseWithDistance;
    [SerializeField] float distRange;
    [SerializeField] bool increaseWithAlign;

    private PostProcessVolume ppv;
    private float intensity = 0f;

    void Start(){
        ppv= GetComponent<PostProcessVolume>();
    }

    void Update(){
        Vector3 dir = transform.position - Camera.main.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        float distPercent = 1f - Mathf.Clamp01(dist/distRange);

        float dot = Mathf.Clamp01(Vector3.Dot(Camera.main.transform.forward, dir));
        
        if(increaseWithDistance&&increaseWithAlign){
            intensity = dot * distPercent;
        }
        else if(increaseWithDistance){
            intensity = distPercent;
        }
        else if(increaseWithAlign){
            intensity = dot;
        }

        
        ppv.weight = intensity;

    }
}
