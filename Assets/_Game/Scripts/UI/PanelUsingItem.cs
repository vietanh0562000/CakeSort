using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelUsingItem : UIPanel
{
    [SerializeField] Image iconItem;
    [SerializeField] TextMeshProUGUI txtTitle;
    [SerializeField] TextMeshProUGUI txtDescript;


    [SerializeField] Button btnClose;
    public override void Awake()
    {
        panelType = UIPanelType.PanelUsingItem;
        base.Awake();
        btnClose.onClick.AddListener(OnClosePanel);
    }
    private void OnEnable()
    {
        btnClose.interactable = true;
    }
    ItemDataCF currentItemData;
    public void OnUseItem(ItemType itemType) {
        currentItemData = ProfileManager.Instance.dataConfig.itemDataConfig.GetItemData(itemType);
        txtTitle.text = currentItemData.title;
        txtDescript.text = currentItemData.description;
        iconItem.sprite = ProfileManager.Instance.dataConfig.spriteDataConfig.GetItemSprite(itemType);
        
        UIManager.instance.panelTotal.UsingItemMode();
        UIManager.instance.panelGamePlay.UsingItemMode();
    }

    public void UsingItemDone() {
        UIManager.instance.panelGamePlay.OutItemMode();
        UIManager.instance.panelTotal.OutItemMode();
        UIManager.instance.ClosePanelUsingItem();
    }

    void OnClosePanel()
    {
        GameManager.Instance.audioManager.PlaySoundEffect(SoundId.SFX_UIButton);
        UIManager.instance.ClosePanelUsingItem();
        GameManager.Instance.itemManager.UsingItemDone();
        EventManager.TriggerEvent(EventName.UsingFillUpDone.ToString());
        EventManager.TriggerEvent(EventName.UsingHammerDone.ToString());
    }

    public void OnUsingItem() {
        btnClose.interactable = false;
    }

    public void OnUsingItemDone() {
        btnClose.interactable = true;
    }
}
