using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Punch : MonoBehaviour
{
    public float velPunch = 1f,
                 dmg = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = transform.position + -transform.right * velPunch * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
