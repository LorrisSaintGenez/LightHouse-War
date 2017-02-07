using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overflow : MonoBehaviour {

    public GameObject borders;
    public GameObject waterLevel;
    public GameObject[] boats;

    private bool flooded;

	// Use this for initialization
	void Start () {
        flooded = false;
        InvokeRepeating("waterRising", 10, 0.25f);
    }

    // Update is called once per frame
    void Update () {
        if (waterLevel.transform.position.y == 0)
        {
            CancelInvoke();
            flooded = true;
        }
    }

    void waterRising()
    {
        float waterLevelUp = 0.25f;
        waterLevel.transform.position = new Vector3(waterLevel.transform.position.x, waterLevel.transform.position.y + waterLevelUp, waterLevel.transform.position.z);
        if (waterLevel.transform.position.y >= -100)
        {
            borders.transform.position = new Vector3(borders.transform.position.x, borders.transform.position.y + waterLevelUp, borders.transform.position.z);
            foreach (GameObject b in boats)
            {
                b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y + waterLevelUp, b.transform.position.z);
            }
        }
    }

    public bool isFlooded()
    {
        return flooded;
    }
}
