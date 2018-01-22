using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public Image _ItemImg;
    private bool _ItemCheak;

    public Sprite _ItemSprite;
    public Sprite _ItemSpriteDelete;
    public static bool m_bItemSelect;
    public static int m_nItemNumber = 1;

    public Text _ItemText;

    void Start ()
    {
        //for (int i = 0; i < 1; i++)
        //{
        //    _ItemImg[i] = GetComponent<Image>();
        //    _ItemCheak[i] = false;
        //}
    }
	
	void Update ()
    {
        if (m_nItemNumber <= 0)
        {
            _ItemImg.sprite = _ItemSpriteDelete;
        }
        else
        {
            if (m_nItemNumber >= 2)
                _ItemText.text = m_nItemNumber.ToString();
            else
                _ItemText.text = " ";

            if (m_bItemSelect)
            {
                AddItem();
                m_bItemSelect = false;
            }
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
            _ItemCheak = true;
            _ItemImg.sprite = _ItemSprite;
        }
    }
}
