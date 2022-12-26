using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private List<CinemachineVirtualCamera> VC = new List<CinemachineVirtualCamera>();

    float MouseZoomSpeed = 15.0f;
    float ZoomMinBound = 0.1f;
    float ZoomMaxBound = 179.9f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            
            VC.Add(transform.GetChild(i).GetComponent<CinemachineVirtualCamera>());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomOp(-scroll, MouseZoomSpeed);

    }

    private void ZoomOp(float deltaMagDiff, float speed)
    {
        for (int i = 0; i < VC.Count; i++)
        {
            VC[i].m_Lens.FieldOfView += deltaMagDiff * speed;
            VC[i].m_Lens.FieldOfView = Mathf.Clamp(VC[i].m_Lens.FieldOfView, ZoomMinBound, ZoomMaxBound);
        }
    }
}
