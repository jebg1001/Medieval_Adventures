using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject prefab1, prefab2, prefab3, prefab4, prefab5, prefab6, generated;
    public Transform hand;

    public static float quantity=0f ;

    public GameObject instantiate(GameObject prefab)
    {
        return Instantiate(prefab, transform.position, Quaternion.identity);
    } 

    // Update is called once per frame
    void Update()
    {

        if(quantity<5)
        {
            switch (Random.Range(1, 7))
            {
                case 1: 
                    generated=instantiate(prefab1);
                    generated.transform.parent = hand;
                    break;
                case 2:
                    generated = instantiate(prefab2);
                    generated.transform.parent = hand;
                    break;
                case 3:
                    generated = instantiate(prefab3);
                    generated.transform.parent = hand;
                    break;
                case 4:
                    generated = instantiate(prefab4);
                    generated.transform.parent = hand;
                    break;
                case 5:
                    generated = instantiate(prefab5);
                    generated.transform.parent = hand;
                    break;
                case 6:
                    generated = instantiate(prefab6);
                    generated.transform.parent = hand;
                    break;
            }
            quantity++;
        }
    }
}
