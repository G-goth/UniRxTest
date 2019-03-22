using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public static class ListExtensions
{
    /// <summary>
    /// List<T>にすでに追加されている要素を省いて追加
    /// </summary>
    /// <param name="addObj">追加したい要素</param>
    /// <typeparam name="T">任意のパラメーター</typeparam>
    /// <returns>要素を省いて追加してList<T>を返す</returns>
    public static void AddTriming<T>(this IList<T> extList, T addObj)
    {
        if(extList.Contains(addObj) == true)
        {
            // 何もしない
        }
        else
        {
            extList.Add(addObj);
        }
    }

    /// <summary>
    /// 制限以内のオブジェクト数をList<T>にすでに追加されている要素を省いて追加
    /// </summary>
    /// <param name="extList">追加したい要素</param>
    /// <param name="addObj">任意のパラメーター</param>
    /// <param name="limit">List<T>に追加するデータの制限</param>
    /// <typeparam name="T"></typeparam>
    public static void AddTrimingLimited<T>(this IList<T> extList, T addObj, int limit)
    {
        if(extList.Contains(addObj) == true)
        {
            // 何もしない
        }
        else if(extList.Count < limit)
        {
            extList.Add(addObj);
        }
    }
}

public class MouseBehaviour : MonoBehaviour
{
    [SerializeField]
    private Material _material = (default);
    [SerializeField]
    private Material _defMaterial = (default);
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
        // Updateストリームに登録
        this.UpdateAsObservable()
            .Subscribe(_ => {
                Debug.Log("objectList Count is " + objectList.Count);
            });
        // マウスホールド時の挙動
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ => {
                GetObjectByRayCastHit();
            });

        // マウスクリックリリース時の挙動
        this.UpdateAsObservable()
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
    
    private void GetObjectByRayCastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 100.0f))
        {
            objectList.AddTriming(hit.collider.gameObject);
            // objectList.AddTrimingLimited(hit.collider.gameObject, 3);
            foreach(var obj in objectList)
            {
                obj.GetComponent<Renderer>().material = _material;
            }
        }
    }
}