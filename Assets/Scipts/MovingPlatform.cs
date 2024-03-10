using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 moveAmount;
    [SerializeField] float speed;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private bool startMove = false;
    private float distanceTotal;
    private float moved = 0f;
    private float percentMoved = 0f;
    
    void Start(){
        startPoint = transform.position;
        endPoint = startPoint+moveAmount;
        direction = (startPoint - endPoint).normalized;
        distanceTotal = Vector3.Distance(startPoint,endPoint);
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            startMove = true;
        }   
    }
    void Update(){
        if(startMove&&percentMoved<=1f){
            moved += speed * Time.deltaTime;
        }
        else if(!startMove&&percentMoved>0f){
            moved -= speed * Time.deltaTime;
        }
        percentMoved = moved/distanceTotal;
        transform.position = Vector3.Lerp(startPoint, endPoint, percentMoved);
        if(percentMoved>=1){
            startMove = false;
        }
    }
}
