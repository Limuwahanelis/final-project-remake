using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ringhandle : MonoBehaviour
{
    public Transform handle;
    private Vector3 normalized;
    private void Update()
    {
        normalized = Vector3.Normalize(handle.transform.position - transform.position);
        Vector3 handlePos = transform.position + normalized*2;
        handle.transform.position = handlePos;
    }

    public Vector3 GetPushVector()
    {
        return normalized / 2;
    }

}
