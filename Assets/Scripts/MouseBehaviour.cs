using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MouseBehaviour : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 currentScreenPoint;
    private Vector3 currentPosition;
    private BoolReactiveProperty isClick = new BoolReactiveProperty();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // マウスクリック時の座標を取得
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => { GetClickObjectName(); });

        // IObservable<(Vector3 screenPoint, Vector3 offset)> clickVector3 = Observable
        //     .EveryUpdate()
        //     .Where(_ => Input.GetMouseButtonDown(0))
        //     .Select(_ => {
        //         return ClickVector3();
        //     });
        // clickVector3.Subscribe(clickOffset => { GetClickObjectName(); }).AddTo(gameObject);
    }

    /// <summary>
    /// クリックした位置を取得
    /// </summary>
    /// <returns>クリックした位置とそのオフセット値を返す</returns>
    private (Vector3 screenPoint, Vector3 offset) ClickVector3()
    {
        (Vector3 screenPoint, Vector3 offset)vector3Tuple;
        vector3Tuple.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        vector3Tuple.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, vector3Tuple.screenPoint.z));
        return vector3Tuple;
    }
    private void GetClickObjectName()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 100.0f))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

}