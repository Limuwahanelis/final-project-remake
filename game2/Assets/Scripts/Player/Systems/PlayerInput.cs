using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private PlayerInteract _playerInteract;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _playerInteract = GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        Move(direction);
        if (Input.GetButtonDown("Attack")) Attack();
        if (Input.GetButtonDown("Jump"))
        {
            
            if (direction * _player.mainBody.transform.localScale.x > 0 && Input.GetKey(KeyCode.DownArrow)) Slide();
            else Jump();
        }
        if (Input.GetButtonDown("Interact")) Interact();
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

    private void Interact()
    {
        _playerInteract.InteractWithObject();
    }
}
