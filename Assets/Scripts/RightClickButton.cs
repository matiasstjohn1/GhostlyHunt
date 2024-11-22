using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickButton : MonoBehaviour, IPointerClickHandler
{
    public AttributesItems itemAttributes;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemAttributes != null)
            {
                itemAttributes.ActiveInfo();
            }
            else
            {
                Debug.LogWarning("FALLA");
            }
        }
    }
}
