using UnityEngine;

[ExecuteInEditMode]
public class LockAtCamera : MonoBehaviour
{
    public bool xFixed;
    public bool yFixed;
    public bool zFixed;

    

    private void LateUpdate()
    {
        Quaternion rotation = transform.rotation;

        Quaternion newRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        if (xFixed)
            newRotation.x = rotation.x;
        if (yFixed)
            newRotation.y = rotation.y;
        if (zFixed)
            newRotation.z = rotation.z;

        transform.rotation = newRotation;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
            LateUpdate();
    }
#endif
}
