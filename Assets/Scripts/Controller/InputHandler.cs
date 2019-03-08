using UnityEngine;

namespace Controller
{
    public class InputHandler : MonoBehaviour
    {
        private float _horizontal;
        private float _vertical;

        private bool _jump;

        private PlayerController _controller;

        private void Start()
        {
            _controller = GetComponent<PlayerController>();
            _controller.Init();
        }

        private void Update()
        {
            GetInput();
            UpdateStates();
            _controller.Tick();
        }

        private void GetInput()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");

            _jump = Input.GetButtonDown("Jump");
        }

        private void UpdateStates()
        {
            _controller.horizontal = _horizontal;
            _controller.vertical = _vertical;
            _controller.jump = _jump;
        }
    }
}
