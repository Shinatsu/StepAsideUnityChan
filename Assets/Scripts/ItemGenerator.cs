using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour {
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 120;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //発展課題
    //item生成間隔
    [SerializeField]
    private float itemCreateInterval;
    //予約生成距離
    [SerializeField]
    private float preloadDistance;
    //ユニティちゃん座標取得用ゲームオブジェクト
    private GameObject unityChan;
    //itemカウント
    private int itemLineCount;


    // Use this for initialization
    void Start () {
        unityChan = GameObject.FindWithTag ("UnityChan");
        itemLineCount = 0;
    }

    // Update is called once per frame
    void Update () {
        float preloadPoint = startPos + (itemLineCount * itemCreateInterval);
        float checkPoint = unityChan.transform.position.z + preloadDistance;


        if (checkPoint >= preloadPoint && preloadPoint < goalPos) {
            CreateItems (preloadPoint);
        }
    }

    void CreateItems(float _posZ){
        //どのアイテムを出すのかをランダムに設定
        int num = Random.Range (0, 10);
        if (num <= 1) {
            //コーンをx軸方向に一直線に生成
            for (float j = -1; j <= 1; j += 0.4f) {
                StartCoroutine(CreateItem(conePrefab, new Vector3 (posRange * j, conePrefab.transform.position.y, _posZ)));
            }
        } else {
            //レーンごとにアイテムを生成
            for (int j = -1; j < 2; j++) {
                //アイテムの種類を決める
                int item = Random.Range (1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6) {
                    //コインを生成
                    StartCoroutine(CreateItem(coinPrefab, new Vector3 (posRange * j, coinPrefab.transform.position.y, _posZ + offsetZ)));
                } else if (7 <= item && item <= 9) {
                    //車を生成
                    StartCoroutine(CreateItem(carPrefab, new Vector3 (posRange * j, carPrefab.transform.position.y, _posZ + offsetZ)));
                }
            }
        }
        itemLineCount++;
    }

    IEnumerator CreateItem(GameObject _prefab, Vector3 _pos){

        yield return new WaitForEndOfFrame ();

        GameObject gObject = Instantiate (_prefab) as GameObject;
        gObject.transform.position = _pos;

    }
}
