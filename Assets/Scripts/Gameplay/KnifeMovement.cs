using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float _deceleration, _jumpForce, _speed, angularVelocity;
    private bool _jumpCompleted, _willJumpAgain;

    [SerializeField] private float _rotateThreshold, _rotationDuration;
    private float _amountToRotate, _baseAmountToRotate, _currentRotationValue;
    private float _desiredDuration, _elapsedTime, _percentageComplete;
    private float _startRotation;

    private bool _rotating;
    private bool _canJumpAgain;
    private float _nextRotation;

    private bool _stuck;

    private bool _gameStarted = false;
    private bool _canStuck = true;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetBaseValues();
    }

    private void SetBaseValues()
    {
        _jumpCompleted = true;
        _baseAmountToRotate = 375;
        _amountToRotate = 375;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_gameStarted)
            {
                _gameStarted = true;
                _rb.isKinematic = false;
                GameObject.Find("Tap To Start").SetActive(false);
            }

            OnRotationalThreshold();

            Jump();
        }
    }

    private void FixedUpdate()
    {
        DecelerateKnife();
    }

    private void DecelerateKnife()
    {
        if (_rb.velocity.x > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x - (Time.deltaTime * _deceleration), _rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (_stuck) return;
        FindObjectOfType<BaseAudioPlayer>().PlayJumpSound();
        _rb.isKinematic = false;

        AddForcesToBlade();

        if (OnRotationalThreshold())
        {
            _willJumpAgain = true;
        }

        if (_jumpCompleted)
        {
            _rotating = true;
            _canJumpAgain = false;
            _jumpCompleted = false;

            StartCoroutine(RotateBlade(_rotationDuration, false));
        }
    }

    private void AddForcesToBlade()
    {

        _rb.velocity = new Vector2(0f, 0f);
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _rb.AddForce(Vector3.right * (_speed), ForceMode.Impulse);
    }

    public IEnumerator RotateBlade(float duration, bool isKnockback)
    {
        StartRotation();

        ResetAngularVelocity();

        _desiredDuration = duration / 1.25f;
        _currentRotationValue = transform.rotation.z;

        float targetRotationValue = GetRotationValue(isKnockback);

        while (_currentRotationValue != targetRotationValue && !_stuck)
        {
            _elapsedTime += Time.deltaTime;
            _percentageComplete = _elapsedTime / _desiredDuration;

            _nextRotation = Mathf.Lerp(StartRotation(), targetRotationValue, _percentageComplete);
            this.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _nextRotation);
            _currentRotationValue = _nextRotation;


            yield return null;
        }

        ResetRotationalValues();

        if (_willJumpAgain)
        {
            _willJumpAgain = false;
            StartCoroutine(RotateBlade(_rotationDuration, false));
        }
        else
        {
            _jumpCompleted = true;
        }

        _nextRotation = 0;
        OnRotationalThreshold();

        _rb.angularVelocity = new Vector3(0, 0, -angularVelocity);
    }

    public void ResetAngularVelocity()
    {
        _rb.angularVelocity = new Vector3(0, 0, 0);
    }

    private float GetRotationValue(bool isKnockback)
    {
        float targetRotationValue;
        if (!isKnockback)
        {
            targetRotationValue = -_amountToRotate;
        }
        else
        {
            targetRotationValue = _amountToRotate;
        }

        return targetRotationValue;
    }

    private void ResetRotationalValues()
    {
        _currentRotationValue = 0;
        _amountToRotate = _baseAmountToRotate;
        _elapsedTime = 0;
        _percentageComplete = 0;
        _rotating = false;
        _canJumpAgain = false;
    }

    private bool OnRotationalThreshold()
    {
        if (_nextRotation <= -_rotateThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float StartRotation()
    {
        _startRotation = transform.rotation.z;
        return _startRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiplier") && _canStuck)
        {
            DisableMovement();
            _rb.isKinematic = true;
            other.GetComponent<ScoreMultiplierInfo>().MultiplyScore();
        }

        if (other.CompareTag("Killer"))
        {
            DisableMovement();
            GameManager.Instance.ShowLoseScreen();
        }
    }

    private void DisableMovement()
    {
        _canStuck = false;
        _stuck = true;
    }
}
