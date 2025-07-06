using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavBarItem : MonoBehaviour
{
    [SerializeField] Button itemBtn;
    public RectTransform rectTransform;
    [SerializeField] Image iconImg;
    public Transform position;
    public Transform lowPosition;
    [SerializeField] Transform iconTrs;
    UnityAction navBarCallBack;
    [SerializeField] Image activeImg;
    [SerializeField] Image inactiveImg;

    void Start()
    {
        //itemBtn.onClick.AddListener(ButtonOnClick);
    }

    public void SetupButton(UnityAction callBack)
    {
        navBarCallBack = callBack;
    }

    public void ButtonOnClick()
    {
        if(navBarCallBack != null) {  navBarCallBack(); }
    }

    public void OnSelect()
    {
        activeImg.gameObject.SetActive(true);
        inactiveImg.gameObject.SetActive(false);
        iconTrs.DOScale(1.45f, 0.3f).SetEase(Ease.InOutBack);
    }
    public void OnDeselect()
    {
        activeImg.gameObject.SetActive(false);
        inactiveImg.gameObject.SetActive(true);
        iconTrs.DOScale(1, 0.25f).SetEase(Ease.OutBack);
    }
}
