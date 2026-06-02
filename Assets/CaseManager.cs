using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{

    [System.Serializable]
    public struct CaseSteps
    {
        public bool step1;
        public bool step2;
        public bool step3;
        public bool step4;
    }

    public CaseSteps _caseSteps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
