using System;
using UniRx;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 currentScreenPoint;
    private Vector3 currentPosition;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // マウスクリック時の座標を取得
        IObservable<(Vector3 screenPoint, Vector3 offset)> clickVector3 = Observable
            .EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Select(_ => {
                (Vector3 screenPoint, Vector3 offset)vector3Tuple;
                vector3Tuple.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                vector3Tuple.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, vector3Tuple.screenPoint.z));
                return vector3Tuple;
            });
        clickVector3.Subscribe(clickOffset => {
            offset = clickOffset.offset;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray.origin, ray.direction, out hit, 100.0f))
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            }).AddTo(gameObject);

        // マウスドラッグ時の値を取得
        // Vector3 currentScreenPoint;
        // Vector3 currentPosition;
        // IObservable<Vector3> mouseDragVector3 = Observable
        //     .EveryUpdate()
        //     .Where(_ => Input.GetMouseButton(0))
        //     .Select(_ => {
        //         currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //         currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + offset;
        //         return transform.position = currentPosition;
        //     });
        // mouseDragVector3.Subscribe(_ => Debug.Log("")).AddTo(gameObject);
        // クリック回数を保持して取得
        // IObservable<int> clickStream = Observable
        //     .EveryUpdate()
        //     .Where(_ => Input.GetMouseButtonDown(0))
        //     .Select(_ => 1)
        //     .Scan((acc, current) => acc + current);        
        // clickStream.Subscribe(clickCount => Debug.Log(clickCount)).AddTo(gameObject);

        // UpdateAsObservableはComponentに対する
        // 拡張メソッドとして定義されているため、呼び出す際は
        // "this."が必要
        // this.UpdateAsObservable().Subscribe(_ => Debug.Log("Update!"));
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
    }
}