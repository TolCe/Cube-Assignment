using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public InputContainer InputContainer;
    private bool _shouldGetInput;
    private bool _pressing;
    private bool _firstPress;
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
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!_pressing)
                {
                    if (_firstPress)
                    {
                        GameEvents.Instance.StartedPlaying();
                        _firstPress = false;
                    }

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
        _shouldGetInput = true;
        _firstPress = true;
    }

    private void OnStartedPlaying()
    {
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
