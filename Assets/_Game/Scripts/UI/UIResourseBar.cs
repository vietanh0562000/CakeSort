using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResourseBar : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    [SerializeField] TextMeshProUGUI amountTxt;
    [SerializeField] Animator myAnim;
    void Start()
    {
        switch (itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Trophy:
                EventManager.AddListener(EventName.ChangeCoin.ToString(), UpdateValue);
                break;
            case ItemType.Coin:
                EventManager.AddListener(EventName.ChangeCoin.ToString(), UpdateValue);
                break;
            case ItemType.Swap:
                break;
            case ItemType.Hammer:
                break;
            case ItemType.ReRoll:
                break;
            default:
                break;
        }
        UpdateValue();
    }

    void UpdateValue()
    {
        switch (itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Trophy:
                amountTxt.text = ProfileManager.Instance.playerData.playerResourseSave.trophy.ToString();
                break;
            case ItemType.Coin:
                amountTxt.text = ProfileManager.Instance.playerData.playerResourseSave.coins.ToString();
                break;
            case ItemType.Swap:
                break;
            case ItemType.Hammer:
                break;
            case ItemType.ReRoll:
                break;
            default:
                break;
        }
        myAnim.SetTrigger("Active");
    }
}
