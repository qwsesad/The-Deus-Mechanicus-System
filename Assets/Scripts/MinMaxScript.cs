using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMaxScript : MonoBehaviour
{
    [SerializeField] private Transform Parent;
    private InputField min;
    private InputField max;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in Parent)
        {
            if(child.name == "MinSize")
            {
                min = child.gameObject.GetComponent<InputField>();
            }
            if(child.name == "MaxSize")
            {
                max = child.gameObject.GetComponent<InputField>();
            }
        } 
    }

    public string getValues()
    {
        double minValue = 0, maxValue = 9999;

        if( min.text != "" )
            minValue = double.Parse(min.text); 

        if( max.text != "" )
            maxValue = double.Parse(max.text);

        string output = "[\"" + minValue + "\",\"" + maxValue + "\"]";

        return output;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
