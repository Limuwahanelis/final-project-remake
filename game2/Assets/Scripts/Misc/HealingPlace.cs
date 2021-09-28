using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlace : MonoBehaviour
{
    [SerializeField]
    private float _healDelay;
    public HealthBar playerHealthBar;
    public IntReference playerCurrentHealth;
    public IntReference playerMaxHP;
    private bool _isHealing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.enabled)
        {
            if (playerCurrentHealth.value < playerMaxHP.value) StartCoroutine(HealPlayerCor());
        }

    }

    IEnumerator HealPlayerCor()
    {
        if (_isHealing) yield break;
        _isHealing = true;
        playerCurrentHealth.value += 1;
        playerHealthBar.SetHealth(playerCurrentHealth.value);
        yield return new WaitForSeconds(_healDelay);
        _isHealing = false;
    }
}
