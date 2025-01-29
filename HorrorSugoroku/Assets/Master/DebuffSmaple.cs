using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebffSmaple : MonoBehaviour
{
    [SerializeField] private Master_Debuff Debuffsheet;
    void Start()
    {
        Debug.Log("aaa");
       Debug.Log(Debuffsheet.DebuffSheet[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
