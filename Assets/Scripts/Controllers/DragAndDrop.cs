using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _isDrag = false;
    private GameObject _dragObject;
    private int _defaultLayerMask;
    [SerializeField] private ConstructionManager _constructionManager;

    private void Awake()
    {
       _defaultLayerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        if (_isDrag)
            Drag();
    }

    public void StartDrag(GameObject gameObject)
    {
        _isDrag = true;
        _dragObject = gameObject;
    }

    private void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, _defaultLayerMask))
        {
            Vector3 worldPos = hitInfo.point;
            _dragObject.transform.position = worldPos;
            CheckMouseInput();
        }
    }

    private void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Drop();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CancelDrag();
        }
    }

    private void Drop()
    {
        if (CanPlaceBuilding())
        {
            _constructionManager.EndBuilding();
            _isDrag = false;
            _dragObject = null;
        }   
    }

    private void CancelDrag()
    {
        _isDrag = false;
        Destroy(_dragObject);
    }

    public bool CanPlaceBuilding()
    {
        Collider collider = _dragObject.GetComponent<Collider>();
        Bounds bounds = collider.bounds;
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity);

        foreach (var col in colliders)
        {
            if (col == collider) continue;
            if (col.CompareTag("Building")) 
                return false;
        }
        return true; 
    }
}
