using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.Utilities;

public class ARInputModule : MonoBehaviour
{        
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARRaycastManager arRaycastManager;
            
    private IDraggable draggable;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    public void Initialize(IDraggable draggable)
    {                        
        this.draggable = draggable;        
    }

    private void Update()
    {
        if (Touch.activeFingers.Count == 1) Drag(Touch.activeTouches);

        else if (Touch.activeFingers.Count == 2) Rotate(Touch.activeTouches);
    }

    private void Drag(ReadOnlyArray<Touch> touch)
    {
        if (touch.Count < 1) return;

        if (touch[0].phase == TouchPhase.Moved)
        {
            Ray ray = arCamera.ScreenPointToRay(touch[0].screenPosition);

            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject))
            {
                IDraggable placementObject = hitObject.transform.GetComponent<IDraggable>();

                draggable.Drag(touch[0].delta.x, touch[0].delta.y);
            }
        }
        
    }

    private void Rotate(ReadOnlyArray<Touch> touch)
    {
        if (touch.Count < 1) return;

        if (touch[0].phase == TouchPhase.Moved)
        {
            Ray ray = arCamera.ScreenPointToRay(touch[0].screenPosition);

            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject))
            {
                IDraggable placementObject = hitObject.transform.GetComponent<IDraggable>();

                draggable.Rotate(touch[0].delta.x, touch[0].delta.y);
            }
        }
    }
}
