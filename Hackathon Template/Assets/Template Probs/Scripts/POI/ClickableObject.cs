using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    [Header("Events Ran On Click")]
    [SerializeField] UnityEvent Events;
    private RaycastHit hit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ClickFunction();
                }
            }
        }
    }


    void ClickFunction()
    {
        print("Clicked");
        Events.Invoke();
    }
}
