using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisable : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(decayTimer());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator decayTimer()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
