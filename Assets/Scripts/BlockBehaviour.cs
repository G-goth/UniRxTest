using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

interface IRecievedGroup : IEventSystemHandler
{
    void OnRecieved(GameObject objName);
}

public class BlockBehaviour : MonoBehaviour, IRecievedGroup
{
    private List<GameObject> objectList = new List<GameObject>();
    private List<Renderer> rendererList = new List<Renderer>();
    
    // Start is called before the first frame update
    void Start()
    {
        objectList = GameObject.FindGameObjectsWithTag("Cube").ToList();
        rendererList = objectList.Select(obj => obj.GetComponent<Renderer>()).ToList();
    }

    public void OnRecieved(GameObject obj)
    {
        // Debug.Log(obj.name);
    }
}
