using UnityEngine;

public class ChildsDestroyer : MonoBehaviour
{
    private void Update()
    {
        if(transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}