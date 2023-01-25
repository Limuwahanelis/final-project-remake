using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private PlayerInteract _playerInteract;
    public GameObject pauseMenu;
    public GameObject darkPanel;
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
                if (Input.GetKeyDown(KeyCode.Escape)) SetPause(_player.isGamePaused.value);
                float direction = Input.GetAxisRaw("Horizontal");
                Move(direction);
                if (Input.GetButtonDown("Attack"))
                {
                    if(Input.GetKey(KeyCode.DownArrow)) DropBomb();
                    else Attack();
                }
                if (Input.GetButtonDown("Jump"))
                {

                    if (direction * _player.mainBody.transform.localScale.x > 0 && Input.GetKey(KeyCode.DownArrow)) Slide();
                    else Jump();
                }
                if (Input.GetButtonDown("Interact")) Interact();

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape)) SetPause(_player.isGamePaused.value);
            }
        }
    }

    private void Move(float direction)
    {
        _player.currentState.Move(direction);
    }
    private void Attack()
    {

        _player.currentState.Attack();

    }
    private void Jump()
    {
        _player.currentState.Jump();
    }

    private void Slide()
    {
        _player.currentState.Slide();
    }
    private void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            _player.isGamePaused.value = false;
            darkPanel.SetActive(false);
            pauseMenu.GetComponent<PauseMenu>().Unpause();
        }
        else
        {
            
            _player.isGamePaused.value = true;
            Time.timeScale = 0f;
            darkPanel.SetActive(true);
            pauseMenu.SetActive(true);
        }

    }
    public void DropBomb()
    {
        _player.currentState.DropBomb();
    }
    
    private void Interact()
    {
        _playerInteract.InteractWithObject();
    }
}
