using AssetKits.ParticleImage.Enumerations;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCakeReward : UIPanel
{
    [SerializeField] RectTransform RawImageRect;
    [SerializeField] RectTransform showingSize;
    [SerializeField] Button closeBtn;
    [SerializeField] Transform tapToContinueTxt;
    public override void Awake()
    {
        panelType = UIPanelType.PanelCakeReward;
        base.Awake();
        RawImageRect.sizeDelta = new Vector2(showingSize.rect.width, showingSize.rect.width);
    }

    private void OnEnable()
    {
        closeBtn.interactable = false;
        DOVirtual.DelayedCall(2.5f, ActiveCloseBtn);
        GameManager.Instance.cakeManager.cakeShowComponent.ShowNewUnlockCake();
        tapToContinueTxt.localScale = Vector3.zero;
        transform.SetAsLastSibling();
    }

    void Start()
    {
        closeBtn.onClick.AddListener(OnClose);
    }

    void ActiveCloseBtn()
    {
        closeBtn.interactable = true;
        tapToContinueTxt.DOScale(1, 0.15f);
    }

    // Update is called once per frame
    void OnClose()
    {
        GameManager.Instance.audioManager.PlaySoundEffect(SoundId.SFX_UIButton);
        UIManager.instance.ClosePanelCakeReward();
        UIManager.instance.ShowPanelSelectReward();
    }
}
