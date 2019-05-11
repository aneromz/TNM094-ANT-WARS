using UnityEngine;

public class AntHome : MonoBehaviour
{
    private Transform sceneTransform;
    private Vector3 positionOffset;
    private Rigidbody model;

    void Start()
    {
        model = GetComponentInChildren<Rigidbody>();
        // The scenes transform used to simulate a parent transform
        sceneTransform = GameObject.Find("Map Content").transform;

        // Correctly position the home on the map
        if (tag == "BlueHome")
            positionOffset = GameObject.Find("BlueHomePosition").transform.localPosition;
        else
            positionOffset = GameObject.Find("RedHomePosition").transform.localPosition;
    }

    void Update()
    {
        model.transform.position = sceneTransform.TransformPoint(positionOffset);
        model.transform.rotation = sceneTransform.rotation;
    }
}
