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
                //if (Input.GetKeyDown(KeyCode.Escape)) SetPause(_player.isGamePaused.value);
                _player.currentState.Move(direction);
                //float direction = Input.GetAxisRaw("Horizontal");
                //Move(direction);
                //if (Input.GetButtonDown("Attack"))
                //{
                //    if(Input.GetKey(KeyCode.DownArrow)) DropBomb();
                //    else Attack();
                //}
                //if (Input.GetButtonDown("Jump"))
                //{

                //    if (direction * _player.mainBody.transform.localScale.x > 0 && Input.GetKey(KeyCode.DownArrow)) Slide();
                //    else Jump();
                //}
                //if (Input.GetButtonDown("Interact")) Interact();

            }
        }
    }
    private void Move(float direction)
    {
        _player.currentState.Move(direction);
    }
    void OnJump(InputValue value)
    {
        if (direction * _player.mainBody.transform.localScale.x > 0 && isDownArrowPressed) _player.currentState.Slide();
        else _player.currentState.Jump();
    }
    void OnVertical(InputValue value)
    {
        direction=value.Get<float>();
    }

    private void OnAttack(InputValue value)
    {
        if(isDownArrowPressed) _player.currentState.DropBomb();
        else _player.currentState.Attack();
    }

    private void OnDownArrowModifier(InputValue value)
    {
        isDownArrowPressed = value.Get<float>()==1?true:false;
    }
    private void OnInteract(InputValue value)
    {
        _playerInteract.InteractWithObject();
    }
}
