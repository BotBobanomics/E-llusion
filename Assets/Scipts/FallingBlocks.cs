using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//use enums to determine different fall types
public class FallingBlocks : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    Renderer r;
    float length;
    float height;
    float width;
    [SerializeField]float heightOffset;
    [SerializeField]float dropDuration;
    [SerializeField]float dropDelay;

    [SerializeField] bool debug = false;
    GameObject blocksParent;

    int totalBlocks;
    int finishedBlocks = 0;
    void Awake(){
        r = GetComponent<Renderer>();
        if(r==null){
            Debug.LogError("Renderer not found on the GameObject");
            return;
        }
        r.enabled = false;
        length = transform.localScale.x;
        height = transform.localScale.y;
        width = transform.localScale.z;
        totalBlocks = Mathf.FloorToInt(length*height*width);
        blocksParent = new GameObject("BlocksParent");
        blocksParent.transform.SetParent(transform,false);
        Vector3 scale = transform.localScale;
        blocksParent.transform.localScale = new Vector3(1/scale.x,1/scale.y,1/scale.z);
        if(debug){
            DebugTool();
        }
    }
    void Start()
    {
        CreateBlocks();
        MoveUp();
        StartCoroutine(DropBlocks(dropDelay));
    }

    void Update()
    {
        if(finishedBlocks==totalBlocks){
            r.enabled = true;
            Destroy(blocksParent);
        }
    }
    void DebugTool(){
        Debug.Log("Length: " + length + ", Height: " + height + ", Width: " + width);
    }
    void CreateBlock(Vector3 targetPos){
        GameObject block = Instantiate(blockPrefab, new Vector3(targetPos.x, targetPos.y, targetPos.z), Quaternion.identity);
        block.transform.SetParent(blocksParent.transform,false);
        block.GetComponent<Renderer>().enabled = false;
    }
    void CreateBlocks(){
        for(int x = 0;x<length;x++){
            for(int y = 0;y<height;y++){
                for(int z = 0;z<width;z++){
                    CreateBlock(new Vector3(x+.5f-length/2,y+.5f-height/2,z+.5f-width/2));
                }
            }
        }
    }
    void MoveUp(){
        blocksParent.transform.Translate(Vector3.up*heightOffset, Space.World);
    }
    //temp: will try to rewrite with interpolation since below is not accurate at small duration
    IEnumerator TranslateOverTime(GameObject block)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dropDuration)
        {
            block.transform.Translate(Vector3.down * (heightOffset / dropDuration) * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        finishedBlocks += 1;
    }
    IEnumerator DropBlocks(float delay){
        for(int i = 0; i<totalBlocks;i++){
            GameObject block = blocksParent.transform.GetChild(i).gameObject;
            block.GetComponent<Renderer>().enabled = true;
            StartCoroutine(TranslateOverTime(block));
            yield return new WaitForSeconds(delay);
        }
    }
}
