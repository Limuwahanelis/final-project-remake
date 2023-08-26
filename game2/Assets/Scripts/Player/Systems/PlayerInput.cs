using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] InputActionAsset controls;
    private PlayerInteract _playerInteract;
    public GameObject pauseMenu;
    public GameObject darkPanel;
    private bool isDownArrowPressed;
    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _playerInteract = GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.isAlive)
        {
            if (!_player.isGamePaused.value)
            {
                _player.currentState.Move(direction);

            }
        }
    }
    private void Move(float direction)
    {
        if (_player.isGamePaused.value) return;
            _player.currentState.Move(direction);
    }
    void OnJump(InputValue value)
    {
        if (_player.isGamePaused.value) return;
        if (direction * _player.mainBody.transform.localScale.x > 0 && isDownArrowPressed) _player.currentState.Slide();
        else _player.currentState.Jump();
    }
    void OnVertical(InputValue value)
    {
        if (_player.isGamePaused.value) return;
        direction =value.Get<float>();
    }

    private void OnAttack(InputValue value)
    {
        if (_player.isGamePaused.value) return;
        if (isDownArrowPressed) _player.currentState.DropBomb();
        else _player.currentState.Attack();
    }

    private void OnDownArrowModifier(InputValue value)
    {
        if (_player.isGamePaused.value) return;
        isDownArrowPressed = value.Get<float>()==1?true:false;
    }
    private void OnInteract(InputValue value)
    {
        if (_player.isGamePaused.value) return;
        _playerInteract.InteractWithObject();
    }
}
