﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARMenuManager : MonoBehaviour
{
    /// <summary>
    /// トラッキング中のARマーカーの種類
    /// </summary>
    public enum TrackingObjectType
    {
        None, Starbucks, Tullys, Wired
    }
    
    [SerializeField] ARTrackedImageManager _arTrackedImageManager;
    [SerializeField] private Text logText;
    
    public static TrackingObjectType TrackingType { get; private set; }

    private void Start()
    {
        // ARFoundationのARTrackedImageManagerの更新タイミングを購読
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void Update()
    {
        logText.text = TrackingType.ToString();
    }

    /// <summary>
    /// ARTrackedImageManagerで読み取った画像の種類によって、追跡中のARマーカーの更新を行う
    /// </summary>
    /// <param name="args"></param>
    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        Debug.Log("Image Changed!");
        
        foreach (var removed in args.removed)
        {
            switch (removed.referenceImage.name)
            {
                case "starbucks":
                    if (TrackingType == TrackingObjectType.Starbucks) TrackingType = TrackingObjectType.None;
                    break;
                case "tullys":
                    if (TrackingType == TrackingObjectType.Tullys) TrackingType = TrackingObjectType.None;
                    break;
                case "wired":
                    if (TrackingType == TrackingObjectType.Wired) TrackingType = TrackingObjectType.None;
                    break;
            }
            
            Debug.Log("Removed : " + removed.name);
        }
 
        foreach (var updated in args.updated)
        {
            switch (updated.referenceImage.name)
            {
                case "starbucks":
                    TrackingType = TrackingObjectType.Starbucks;
                    break;
                case "tullys":
                    TrackingType = TrackingObjectType.Tullys;
                    break;
                case "wired":
                    TrackingType = TrackingObjectType.Wired;
                    break;
            }
            Debug.Log("Updated : " + updated.name);
        }       
        
        foreach (var added in args.added)
        {
            switch (added.referenceImage.name)
            {
                case "starbucks":
                    TrackingType = TrackingObjectType.Starbucks;
                    break;
                case "tullys":
                    TrackingType = TrackingObjectType.Tullys;
                    break;
                case "wired":
                    TrackingType = TrackingObjectType.Wired;
                    break;
            }
            Debug.Log("Added : " + added.name);
        }
    }
}
