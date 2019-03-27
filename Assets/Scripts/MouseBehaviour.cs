using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

public class MouseBehaviour : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        var objectList = GameObject.FindGameObjectsWithTag("Cube").ToList();

        // マウスホールド時の挙動
        var mouseHold = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Select(_ => GetObjectByRayCastHit())
            .Where(_ => GetObjectByRayCastHit() != null)
            .DistinctUntilChanged()
            .Subscribe(_ => {
                ExecuteEvents.Execute<IRecieverGroups>(
                    target: gameObject,
                    eventData: null,
                    functor: (reciever, eventData) => reciever.OnRecieved(GetObjectByRayCastHit())
                );
            });

        // マウスボタンリリース時の挙動
         var mouseRelease = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => {
                ExecuteEvents.Execute<IRecieverGroups>(
                    target: gameObject,
                    eventData: null,
                    functor: (reciever, eventData) => reciever.OnRecievedMaterialAllChange()
                );
            });
    }

    private GameObject GetObjectByRayCastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 100.0f))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}