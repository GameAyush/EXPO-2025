using UnityEngine;

public class FloatyBoiiii : MonoBehaviour
{
    void Update()
    {
        float z = Mathf.PingPong(t: Time.time, length: 1f);
        Vector3 axis = new Vector3(x: 1, y: 1, z);
        transform.Rotate(axis, angle: 1f);
    }
}
