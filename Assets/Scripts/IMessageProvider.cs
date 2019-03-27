using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRxTest.Assets.Scripts
{
    public interface IMessageProvider : IEventSystemHandler
    {
        void OnRecievedOneShotMaterialChange(GameObject obj);
        void OnRecievedMaterialAllChange();
    }
}