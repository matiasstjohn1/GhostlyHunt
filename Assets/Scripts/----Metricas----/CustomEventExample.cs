using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
public class CustomEventExample : MonoBehaviour
{
    public int NpcIDa;
    public int ObjectiveIDa;
    public int ItemsIDa;
    public int GhostlyIDa;


    public string GhostlyTypec;//Nombre del que se equipo
    public float GhostlyTime;//Tiempo en combate
    public float GhostlyEquip; //Tiempo en el equipo
    public int GhostlyTypeInv;


    void Start()
    {  
    }
    public void OnLevelComplete()
    {

        CustomEvent myEvent = new CustomEvent("ObjectivesCompleted")        {
            { "ghostly_id", GhostlyIDa },
            { "items_id", ItemsIDa },
            { "objective_id", ObjectiveIDa },
            { "npc_id", NpcIDa }
        };
        AnalyticsService.Instance.RecordEvent(myEvent);
        //Debug.Log("NO ANDA     " + GhostlyIDa+"  ,  "+ ItemsIDa + "  ,  " + ObjectiveIDa + "  ,  " + NpcIDa);
        Debug.LogError($"Evento Objective_Complete enviado: npc_id={NpcIDa}, objective_id={ObjectiveIDa}, items_id={ItemsIDa}, ghostly_id={GhostlyIDa}");
        NpcIDa = -1;
        ObjectiveIDa = -1;
        ItemsIDa = -1;
        GhostlyIDa = -1;
    }
    public void OnGhostlyEquipedOnCombat()
    {

        CustomEvent myEvent = new CustomEvent("GhostlyEquipedComb"){
            { "ghostly_type_c", GhostlyTypec },
            { "ghostly_time", GhostlyTime },
        };
        AnalyticsService.Instance.RecordEvent(myEvent);
        Debug.LogError($"Evento GhostlyEquipedComb enviado: ghostly_type_c={GhostlyTypec}, ghostly_time={GhostlyTime}");
        GhostlyTime = 0;
        GhostlyTypec = null;
    }

    public void OnGhostlyEquiped()
    {

        CustomEvent myEvent = new CustomEvent("GhostlyEquiped"){
            { "ghostly_type_inv", GhostlyTypeInv },
            { "ghostly_equip", GhostlyEquip },
        };
        AnalyticsService.Instance.RecordEvent(myEvent);
        Debug.LogError($"Evento GhostlyEquipedComb enviado: ghostly_type_inv={GhostlyTypeInv}, ghostly_equip={GhostlyEquip}");
        GhostlyEquip = 0;
        GhostlyTypeInv = -1;
    }


}
