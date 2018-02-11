using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewController : MonoBehaviour {

    public bool isDestroyWhenInvisible = false;

    //描画が開始されたか
    private bool isViewStarted = false;

    //描画中か
    private bool isViewed = false;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (isDestroyWhenInvisible) {
            if(isViewStarted){
                if(!isViewed){
                    StartCoroutine (DestroySelf());
                }
            }
        }
        isViewed = false;
    }

    void OnWillRenderObject(){
#if UNITY_EDITOR
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
#endif
        {
            if (!isViewStarted) isViewStarted = true;

            isViewed = true;
        }
    }

    IEnumerator DestroySelf(){

        yield return new WaitForEndOfFrame ();

        Destroy (this.gameObject);
    }
}
