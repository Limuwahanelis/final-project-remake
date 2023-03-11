using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LogicPuzzle2 : Puzzle
{
    // Start is called before the first frame update
    public InteractableTorch2[] torches;
    public TMP_Text text;
    public int numberToGet = 29;
    int number = 0;
    public GameObject crystal;
    [SerializeField] private bool solved = false;
    public TMP_Text riddleText;
    [SerializeField] float crystalSpeed;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateNumber(bool add,int value)
    {
        if (add) number += value;
        else number -= value;
        text.text = number.ToString();
        if(solved) StartCoroutine(MoveCrystalCor());
        if (number==numberToGet)
        {
            solved = true;
            //gamMan.MarkPuzzleAsSolved(2);
          
        }
    }
    IEnumerator MoveCrystalCor()
    {
        float tmp = Time.deltaTime * crystalSpeed;
        float total = 0;
        while (total <= 6.7)
        {
            crystal.transform.position = new Vector3(crystal.transform.position.x, crystal.transform.position.y - tmp, 0);
            total += tmp;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!solved) riddleText.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!solved) riddleText.enabled = false;
    }

    public override void MarkAsSolved()
    {
        torches[0].LightUp();
        torches[2].LightUp();
        torches[3].LightUp();
        torches[4].LightUp();
        text.text = numberToGet.ToString();
        solved = true;
        StartCoroutine(MoveCrystalCor());
    }
}
