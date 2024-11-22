using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Partner : MonoBehaviour
{
    //Variables pre definidas.//
    Image image1,image2,image3;
    public List<Sprite> sprites; //Acompañantes.
    public GameObject ghostly1,ghostly2,ghostly3;

    private void Start()
    {
        image1 = ghostly1.GetComponent<Image>();
        image2 = ghostly2.GetComponent<Image>();
        image3 = ghostly3.GetComponent<Image>();

    }

    private void Update()
    {
        ChangeSprite();
        //DESACTIVAR//
        if(StatsSave.Instance.currentHealth1 <=0)
        {
            ghostly1.SetActive(false);
        }
        if (StatsSave.Instance.currentHealth2 <= 0)
        {
            ghostly2.SetActive(false);
        }
        if (StatsSave.Instance.currentHealth3 <= 0)
        {
            ghostly3.SetActive(false);
        }
        //ACTIVAR//
        if (StatsSave.Instance.currentHealth1 > 0)
        {
            ghostly1.SetActive(true);
        }
        if (StatsSave.Instance.currentHealth2 > 0)
        {
            ghostly2.SetActive(true);
        }
        if (StatsSave.Instance.currentHealth3 > 0)
        {
            ghostly3.SetActive(true);
        }
    }

    public void ChangeSprite()
    {
        image1.sprite = sprites[StatsSave.Instance._nameIndex1];
        image2.sprite = sprites[StatsSave.Instance._nameIndex2];
        image3.sprite = sprites[StatsSave.Instance._nameIndex3];
    }
}
