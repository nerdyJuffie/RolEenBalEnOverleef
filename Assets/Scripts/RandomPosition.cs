using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public GameObject prefab;
    public int xPos;
    public int yPos;
    public int index;
    public float wait;
    public int objectToAdd;
    private int aantalPaddos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (index < objectToAdd)
        {
            xPos = Random.Range(-300, 300);
            yPos = Random.Range(-300, 300);

            Instantiate(prefab, new Vector3(xPos, 0.0f, yPos), Quaternion.identity);
            yield return new WaitForSeconds(wait);
            index += 1;
        }
    }
   /*** _aantalPaddos = objectToAdd;
    public int aantalPaddos
    {
        get
        {
            _aantalPaddos;
        }
    }***/
}
