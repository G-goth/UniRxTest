using UnityEngine;
using Player;

namespace InputProviders
{
    public class UnityInputProvider : IInputProvider
    {
        public bool GetDash()
        {
            return Input.GetKeyDown(KeyCode.D);
        }

        public bool GetJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public Vector3 GetMoveDirection()
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}