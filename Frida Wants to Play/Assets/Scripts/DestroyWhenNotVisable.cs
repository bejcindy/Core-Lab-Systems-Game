using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenNotVisable : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
