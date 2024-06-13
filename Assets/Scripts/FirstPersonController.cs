using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Player")]
        public float moveSpeed = 4.0f;
        public float rotationSpeed = 1.0f;
        public float speedChangeRate = 10.0f;

        [Space(10)]
        public float jumpHeight = 1.2f;
        public float gravity = -15.0f;

        [Space(10)]
        [Header("Cinemachine")]
        public GameObject cinemachineCameraTarget;
        public float topClamp = 90.0f;
        public float bottomClamp = -90.0f;

        private float _cinemachineTargetPitch;
        private float _speed;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            ProcessFlyAndGravity();
            ProcessCameraRotation();
            ProcessMovement();
        }

        private void ProcessCameraRotation()
        {
            float lookX = Input.GetAxis("Mouse X");
            float lookY = Input.GetAxis("Mouse Y");

            if (Mathf.Abs(lookX) >= _threshold || Mathf.Abs(lookY) >= _threshold)
            {
                float deltaTimeMultiplier = 1.0f;

                _cinemachineTargetPitch += lookY * rotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = lookX * rotationSpeed * deltaTimeMultiplier;

                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

                cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }

        private void ProcessMovement()
        {
            float targetSpeed = moveSpeed;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) < _threshold && Mathf.Abs(Input.GetAxis("Vertical")) < _threshold)
            {
                targetSpeed = 0.0f;
            }

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
            float speedOffset = 0.1f;
            float inputMagnitude = 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;
            if (inputDirection != Vector3.zero)
            {
                inputDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
                GetComponent<Animator>().SetTrigger("Move");
            }
            else{
                GetComponent<Animator>().SetTrigger("Idle");
            }

            _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void ProcessFlyAndGravity()
        {
            if (Input.GetButton("Jump"))
            {
                _verticalVelocity = Mathf.Lerp(_verticalVelocity, moveSpeed, Time.deltaTime * speedChangeRate);
            }
            else
            {
                _verticalVelocity += gravity * Time.deltaTime; // Gradually apply gravity
            }

            if (_verticalVelocity < gravity)
            {
                _verticalVelocity = gravity;
            }

            _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
