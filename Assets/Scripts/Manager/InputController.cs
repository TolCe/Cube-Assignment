using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public InputContainer InputContainer;
    private bool _shouldGetInput;
    private bool _pressing;
    private float _inputAmount;
    private Vector3 _initialMousePosition;

    private void OnEnable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded += OnLevelLoaded;
            GameEvents.Instance.OnStartedPlaying += OnStartedPlaying;
            GameEvents.Instance.OnResetInput += OnResetInput;
            GameEvents.Instance.OnLevelFinished += OnLevelFinished;
        }
    }

    private void OnDisable()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.OnLevelLoaded -= OnLevelLoaded;
            GameEvents.Instance.OnStartedPlaying -= OnStartedPlaying;
            GameEvents.Instance.OnResetInput -= OnResetInput;
            GameEvents.Instance.OnLevelFinished -= OnLevelFinished;
        }
    }

    private void FixedUpdate()
    {
        if (_shouldGetInput)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_pressing)
                {
                    GameEvents.Instance.ResetInput();
                    _pressing = true;
                }

                if (_pressing)
                {
                    Vector3 distance = Input.mousePosition - _initialMousePosition;
                    _inputAmount = distance.x / Screen.width * InputContainer.VO.Sensitivity;

                    GameEvents.Instance.HorizontalMove(_inputAmount, InputContainer.VO.XLimit);
                }
            }
            else
            {
                if (_pressing)
                {
                    GameEvents.Instance.ResetInput();
                    _pressing = false;
                }
            }
        }
    }

    private void OnLevelLoaded(int levelIndex)
    {

    }

    private void OnStartedPlaying()
    {
        _shouldGetInput = true;
    }

    private void OnResetInput()
    {
        _initialMousePosition = Input.mousePosition;
    }

    private void OnLevelFinished()
    {
        _pressing = false;
        _shouldGetInput = false;
    }
}
