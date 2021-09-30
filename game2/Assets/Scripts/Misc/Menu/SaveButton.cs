using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SaveButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text saveDescription;
    public int saveIndex;

    public void SetDescription(string date)
    {
        saveDescription.text = date;
    }
}
