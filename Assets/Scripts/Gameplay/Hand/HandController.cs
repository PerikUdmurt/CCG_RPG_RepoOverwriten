using CCG.Gameplay.Hand;
using System.Collections;
using UnityEngine;

namespace CCG.Gameplay
{
    public class HandController : MonoBehaviour
    {
        [Header("Cconstraints")]
        public bool dragIsAvailable = true;
        public bool useIsAvailable = true;
        public bool selectIsAvailable = true;
        
        [SerializeField] private float _useTriggerTime;
        [SerializeField] private LayerMask _usableLayer;
        
        private Camera _camera => Camera.main;
        private bool _useTrigger = false;
        private bool isDraging = false;
        private Vector3 mousePoint;
        private IDragable currentDragableObj = null;
        private ISelectable currentSelectObj = null;

        private void Update()
        {
            if (_camera != null) { mousePoint = _camera.ScreenToWorldPoint(Input.mousePosition); }

            RayCheck();
        }

        
        private void RayCheck()
        {

            if (!RayDetect().Equals(default(RaycastHit)))
            {
                if (RayDetect().collider.gameObject.TryGetComponent(out IUsable usableObj) && Input.GetMouseButtonDown(0) && useIsAvailable && usableObj.isUsable)
                {
                    Use(usableObj);
                }

                if (RayDetect().collider.gameObject.TryGetComponent(out ISelectable selectableObj)&&selectIsAvailable && selectableObj.isSelectable)
                {
                    Select(selectableObj);
                }
                else if (currentSelectObj != null)
                {
                    Deselect(currentSelectObj);
                    currentSelectObj = null;
                }

                if (RayDetect().collider.gameObject.TryGetComponent(out IDragable dragableObj) && Input.GetMouseButtonDown(0) && dragIsAvailable && dragableObj.isDragable)
                {
                    currentDragableObj = dragableObj;
                    Drag(dragableObj);
                }
            }
            else 
            {
                Deselect(currentSelectObj);
            }

            if (Input.GetMouseButton(0) && isDraging)
            {
                Drag(currentDragableObj);
            }
            
            if (Input.GetMouseButtonUp(0))
            { 
                if (currentDragableObj != null) { Drop(currentDragableObj); }
            }
        }

        private RaycastHit RayDetect()
        {
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _usableLayer)) return hitInfo;
            else return default(RaycastHit);
        }
        
        private void Drag(IDragable dragableObj)
        {
            if(!isDraging)
            {
                dragableObj.Take();
            }
            isDraging = true;
            dragableObj.Drag(new Vector3(mousePoint.x, mousePoint.y, 0));
            Deselect(currentSelectObj);
        }

        private void Drop(IDragable dragableObj)
        {
            dragableObj.Drop();
            currentDragableObj = null;
            isDraging = false;
        }

        
        private void Select(ISelectable selectableObj)
        {
            if (currentSelectObj != selectableObj)
            {
                Deselect(currentSelectObj);
                currentSelectObj = selectableObj;
                selectableObj.Select();
            }
        }

        private void Deselect(ISelectable selectableObj)
        {
            if (currentSelectObj == null) return;
            currentSelectObj.Deselect();
            currentSelectObj = null;
        }

        private void Use(IUsable usableObj)
        {
            usableObj.Use();
        }
    }
}
