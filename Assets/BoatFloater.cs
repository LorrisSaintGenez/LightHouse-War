using UnityEngine;
using System.Collections;

public class BoatFloater : MonoBehaviour {

    private Transform seaPlane;
    private Cloth planeCloth;
    private int closestVertexIndex = -1;

	// Use this for initialization
	void Start () {
        seaPlane = GameObject.Find("Water_Plane").transform;
        planeCloth = seaPlane.GetComponent<Cloth>();
	}
	
	// Update is called once per frame
	void Update () {
        GetClosestVertex();
	}

    void GetClosestVertex ()
    {
        for (int i = 0; i < planeCloth.vertices.Length; i++)
        {
            if (closestVertexIndex == -1)
                closestVertexIndex = 0;
            float distance = Vector3.Distance(planeCloth.vertices[i], transform.position);
            float closestDistance = Vector3.Distance(planeCloth.vertices[closestVertexIndex], transform.position);

            if (distance < closestDistance)
                closestVertexIndex = i;
        }

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            planeCloth.vertices[closestVertexIndex].y / 100,
            transform.localPosition.z
        );
    }
}
