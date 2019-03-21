using UnityEngine;
using InputProviders;

namespace Player
{
    public class Mover : MonoBehaviour
    {
        private IInputProvider inputProvider;
        private GameObject targetGameObject;
        private UnityInputProvider unityInputProvider;

        public void SetInputProvider(IInputProvider input)
        {
            inputProvider = input;
        }
        // Start is called before the first frame update
        void Start()
        {
            inputProvider = ServiceLocatorProvider.Instance.inputCurrent.Resolve<IInputProvider>();
        }

        // Update is called once per frame
        void Update()
        {
            if(inputProvider == null) return;
            if(inputProvider.GetJump())
            {
                Jump();
            }
        }
        /// <summary>
        /// ジャンプする
        /// </summary>
        void Jump()
        {
            Debug.Log("Jump!!");
        }

        /// <summary>
        /// 移動する
        /// </summary>
        /// <param name="direction">移動方向</param>
        /// <param name="isDash">ダッシュするか</param>
        void Move(Vector3 direction, bool isDash)
        {
            Debug.Log("Moving!!");
        }
    }
}