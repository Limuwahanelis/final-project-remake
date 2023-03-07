using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractableTorch : MonoBehaviour,IInteractable
{
    private Light2D mainLight;
    public GameObject fire;
    public GameObject canvas;
    public bool fireActive = false;
    public InteractableTorch torch1;
    public InteractableTorch torch2;
    public int torchIndex;
    public LogicPuzzle1 LogicPuzzle1;
    private PlayerInteract _playerInteract;
    // Start is called before the first frame update
    void Awake()
    {
        mainLight = transform.GetComponentInChildren<Light2D>();
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
        if (mainLight.enabled) return;
        mainLight.enabled = true;
        fireActive = !fireActive;
        fire.SetActive(fireActive);
        torch1.OtherTorchInteract();
        torch2.OtherTorchInteract();
        LogicPuzzle1.CheckIfTorchesAreLit(torchIndex);
    }
    public void OtherTorchInteract()
    {
        fireActive = !fireActive;
        fire.SetActive(fireActive);
        mainLight.enabled = !mainLight.enabled;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
            canvas.SetActive(true);
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.setObjectToInteract(this);
        //gameMan.GetPlayer().GetComponent<PlayerInteract>().setObjectToInteract(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            canvas.SetActive(false);
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.setObjectToInteract(this);
        // gameMan.GetPlayer().GetComponent<PlayerInteract>().RemoveObjectToInteract();
    }
    public bool GetState()
    {
        return fireActive;
    }
    public void LightUp()
    {
        mainLight.enabled = true;
        fire.SetActive(true);
    }
}
