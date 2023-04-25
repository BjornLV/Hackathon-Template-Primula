using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using BVStudios;

public class PointOfInterest : MonoBehaviour
{
    [SerializeField] private string POI_Name = "LEAVE EMPTY";
    [SerializeField] private string POI_Type = "LEAVE EMPTY";

    [Header("POI Objects")]
    [SerializeField] public POIType _poiType;

    [SerializeField] public TMP_Text _POINameTEXT;
    [SerializeField] public TMP_Text _POITypeTEXT;

    [Header("POI Events")]
    [SerializeField] UnityEvent _events;

    private RaycastHit _hit;
    private Vector2 _touchStartPosition;

    public bool POIIsActive = false;

    private void Start()
    {
        Transform childTransform = transform.GetChild(0);
        Transform parentTransform = childTransform.parent;

        if (parentTransform.name == "") { Destroy(parentTransform); return; }

        POI_Name = this.name;
        POI_Type = this.tag;

        SaveSystem.SaveData<string>(POI_Name, _poiType.ToString());
    }

    private void Update()
    {
        _POINameTEXT.text = POI_Name;
        _POITypeTEXT.text = POI_Type;
    }

    public void runPOIFunction()
    {
        _events.Invoke();
    }

    public void printClickStats()
    {
        print("POI Name: " + POI_Name);
        print("POI Type: " + POI_Type);
    }
}

public enum POIType
{
    oneTimePOI,
    regularPOI,
    capturablePOI,
    fractionPOI
}