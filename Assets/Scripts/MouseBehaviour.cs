using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class ListExtensions
{
    /// <summary>
    /// List<T>にすでに追加されている要素を省いて追加してList<T>を返す
    /// </summary>
    /// <param name="addObj">追加したい要素</param>
    /// <typeparam name="T">任意のパラメーター</typeparam>
    /// <returns>要素を省いて追加してList<T>を返す</returns>
    public static void AddTriming<T>(this IList<T> extList, T addObj)
    {
        if(extList.Contains(addObj) != false)
        {
            // 何もしない
        }
        else
        {
            extList.Add(addObj);
        }
    }
}

public class MouseBehaviour : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    [SerializeField]
    private Material _defMaterial;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 currentScreenPoint;
    private Vector3 currentPosition;
    private List<GameObject> objectList = new List<GameObject>();
    private BoolReactiveProperty isClick = new BoolReactiveProperty();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // // マウスクリック時の座標を取得
        // this.UpdateAsObservable()
        //     .Subscribe(_ => {
        //         GetObjectByRayCastHit();
        //     });
        // マウスホールド時の挙動
        this.FixedUpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ => {
                GetObjectByRayCastHit();
            });

        // マウスクリックリリース時の挙動
        this.FixedUpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => {
                foreach(var obj in objectList)
                {
                    obj.GetComponent<Renderer>().material = _defMaterial;
                }
                objectList.Clear();
            });
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
    
    private List<GameObject> GetObjectByRayCastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 100.0f))
        {
            objectList.AddTriming(hit.collider.gameObject);
            foreach(var obj in objectList)
            {
                obj.GetComponent<Renderer>().material = _material;
            }
        }
        return objectList;
    }
    private void GetObjectOnHoldingMouseButton()
    {
    }
}