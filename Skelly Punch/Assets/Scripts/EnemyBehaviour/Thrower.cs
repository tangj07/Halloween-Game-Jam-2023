using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float timeToThrow;
    void Throw()
    {
        Instantiate(projectile, gameObject.transform.position,Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ThrowDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToThrow);
            Throw();
        }
    }
}
