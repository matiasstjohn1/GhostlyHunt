using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class MenuInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void SendEvents(string buttonaction)
    {
        ButtonPressedEvent BtnEvt = new ButtonPressedEvent() { 
            ActionName = buttonaction,
        };

        AnalyticsService.Instance.RecordEvent(BtnEvt);
    }
}
