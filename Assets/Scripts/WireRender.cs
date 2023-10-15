using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRender : MonoBehaviour
{
    public Transform rightHandAnchor = null;
    public LineRenderer lineRenderer = null;
    [SerializeField] private Wire _wire;
 
    void Update ()
    {
        // 右手のコントローラの位置と向いている方向からRayを作成
        Ray laserPointer = new Ray(rightHandAnchor.position, rightHandAnchor.forward);

        renderLaser(laserPointer.origin, _wire.raycastHitpoint);
    }
    

    private void renderLaser(Vector3 from, Vector3 to)
    {
        // Line Rendererの1点目と2点目の位置を指定する
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }
}
