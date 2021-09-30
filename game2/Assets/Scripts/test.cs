using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public class yesyDat
    {
        public List<bool> wasPickUpPicked;

        public yesyDat(yesyDat yesyDat)
        {
            for(int i=0;i< yesyDat.wasPickUpPicked.Count;i++)
            {
                wasPickUpPicked.Add(yesyDat.wasPickUpPicked[i]);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        List<bool> values = new List<bool>();
        values.Add(true);
        values.Add(false);
        //dd = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
