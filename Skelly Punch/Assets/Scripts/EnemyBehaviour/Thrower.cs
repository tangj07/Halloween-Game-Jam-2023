using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float timeToThrow;
    Vector3 position;
    void Throw()
    {
        position.y += 2f;
        position.x += 1;
        Instantiate(projectile, position,Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowDelay());
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;
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
