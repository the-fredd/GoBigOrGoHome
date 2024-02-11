using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera2;
    [SerializeField] PlayerMovement plmov;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera2.m_Lens.OrthographicSize = 4 + (0.5f * Mathf.Max(plmov.transform.localScale.x, plmov.transform.localScale.y));
    }
}
