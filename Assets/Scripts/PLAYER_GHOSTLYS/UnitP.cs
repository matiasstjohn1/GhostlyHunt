using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitP : MonoBehaviour
{
    //Cambiar personaje
    private Image image;
    public List<Sprite> sprites;

    public List<string> unitName;

    public float currentHP;
    public int nameindex;

    private void Start()
    {
        image = GetComponent<Image>();
        currentHP = StatsManager.Instance._maxHP;
    }

    private void Update()
    {
        ChangeSprite(StatsManager.Instance._index);
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > StatsManager.Instance._maxHP)
            currentHP = StatsManager.Instance._maxHP;
    }

    public void ChangeSprite(int index)
    {
        if (index >= 0 && index <= sprites.Count)
        {
            image.sprite = sprites[index];
        }
        else
        {
            Debug.LogError("No hay sprites en Lista.");
        }
    }
}
