using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public static Inventory _instance = null;

    public Image _ItemImg;
    private bool _ItemCheak;

    public Sprite _ItemSprite;
    public Sprite _ItemSpriteDelete;
    public bool m_bItemSelect;
    public int m_nItemNumber = 0;
    public Text _ItemText;

    void Start()
    {
        Inventory._instance = this;
    }
    void Update ()
    {
        Debug.Log("m_nItemNumber : " + m_nItemNumber);

        if (m_nItemNumber <= 0)
        {
            _ItemImg.sprite = _ItemSpriteDelete;
            _ItemText.text = " ";
        }
        else if (m_nItemNumber >= 1)
        {
            _ItemImg.sprite = _ItemSprite;
            _ItemText.text = m_nItemNumber.ToString();
        }

        if (m_bItemSelect)
        {
            AddItem();
            m_bItemSelect = false;
        }
    }

    void AddItem()
    {
        if(_ItemCheak == true)
        {
            m_nItemNumber++;
        }
        else
        {
            m_nItemNumber++;
            _ItemCheak = true;
            _ItemImg.sprite = _ItemSprite;
        }
    }
}
