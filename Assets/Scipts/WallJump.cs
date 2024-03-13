using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] float xAmount;
    [SerializeField] float yAmount;
    [SerializeField] float mult;
    [SerializeField] float velocityLimit = 20f;
    private Vector3 wallNormal;
    void OnCollisionStay(Collision other){
        //While touching wall, calculate normal of contact point and push in the opposite direction when space is pushed. 
        if (other.contacts.Length > 0 && other.gameObject.CompareTag("isWall")){
            wallNormal = other.contacts[0].normal;     
            if(wallNormal!=Vector3.up){
                if(Input.GetKey(KeyCode.Space)){
                //Debug.Log(wallNormal);
                Vector3 jumpDirection = (wallNormal * xAmount)+(Vector3.up * yAmount);
                jumpDirection = jumpDirection.normalized;
                PlayerManager.Instance.pushPlayer(mult,jumpDirection,velocityLimit);
                }
            }  
        }
    }
}
