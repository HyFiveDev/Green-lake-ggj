using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grappler : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    void Start()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider is PolygonCollider2D)
            {
                _lineRenderer.SetPosition(0, hit.point);
                _lineRenderer.SetPosition(1, transform.position);

                _distanceJoint.connectedAnchor = hit.point;
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}