using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System;



public class AnalyticsManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
    void GiveConsent()
    {
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"consent given! We can get data!!!!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
