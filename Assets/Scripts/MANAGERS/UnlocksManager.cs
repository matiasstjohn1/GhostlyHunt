using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnlocksManager : MonoBehaviour
{
    // Singleton instance
    public static UnlocksManager instance;

    // Lista de objetos desbloqueables
    [SerializeField] List<GameObject> unlockableObjects;
    // Lista de locks
    [SerializeField] List<GameObject> lockObjects;
    // HashSet para rastrear objetos ya desbloqueados
    private HashSet<GameObject> unlockedObjects;

    private void Awake()
    {
        // Configurar el singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        unlockedObjects = new HashSet<GameObject>();
    }

    public void Unlock(GameObject unlockable)
    {
        if (!unlockedObjects.Contains(unlockable))
        {
            unlockedObjects.Add(unlockable);

            //Activar el objeto en el diario o en la UI
            unlockable.SetActive(true);
        }
    }
    public void Lock(GameObject unlockable)
    {
        if (!unlockedObjects.Contains(unlockable))
        {
            unlockedObjects.Add(unlockable);

            //Activar el objeto en el diario o en la UI
            unlockable.SetActive(false);
        }
    }
    public bool IsUnlocked(GameObject unlockable)
    {
        return unlockedObjects.Contains(unlockable);
    }

    public void UnlockByIndex(int index)
    {
        if (index >= 0 && index < unlockableObjects.Count)
        {
            Unlock(unlockableObjects[index]);
            Lock(lockObjects[index]);
        }
    }
}
