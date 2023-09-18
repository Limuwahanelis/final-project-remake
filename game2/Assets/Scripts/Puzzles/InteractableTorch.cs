using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractableTorch : MonoBehaviour,IInteractable
{
    public GameObject fire;
    public GameObject canvas;
    public bool fireActive = false;
    public InteractableTorch torch1;
    public InteractableTorch torch2;
    public int torchIndex;
    public LogicPuzzle1 LogicPuzzle1;
    private Light2D[] mainLights;
    private PlayerInteract _playerInteract;
    private bool _canInteract = true;
    // Start is called before the first frame update
    void Awake()
    {
        mainLights = transform.GetComponentsInChildren<Light2D>();
    }
    void Start()
    {
        LogicPuzzle1 = transform.GetComponentInParent<LogicPuzzle1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (!_canInteract) return;
        foreach (var light in mainLights) light.enabled = true;
        fireActive = !fireActive;
        fire.SetActive(fireActive);
        torch1.OtherTorchInteract();
        torch2.OtherTorchInteract();
        LogicPuzzle1.CheckIfTorchesAreLit(torchIndex);
        SetInteraction(!fireActive);
    }
    public void OtherTorchInteract()
    {
        fireActive = !fireActive;
        _canInteract = !_canInteract;
        fire.SetActive(fireActive);
        foreach (var light in mainLights) light.enabled = !light.enabled;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canInteract) return;
        Debug.Log(collision.tag);
            canvas.SetActive(true);
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.setObjectToInteract(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_canInteract) return;
        canvas.SetActive(false);
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.RemoveObjectToInteract();
    }
    public void SetInteraction(bool value)
    {
        _canInteract = false;
        if(!_canInteract)
        {
            canvas.SetActive(false);
            if(_playerInteract) _playerInteract.RemoveObjectToInteract();

        }
    }
    public bool GetState()
    {
        return fireActive;
    }
    public void LightUp()
    {
        foreach (var light in mainLights) light.enabled = true;
        fire.SetActive(true);
    }
}
