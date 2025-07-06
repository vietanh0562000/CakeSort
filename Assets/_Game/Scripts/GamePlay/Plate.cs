using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class Plate : MonoBehaviour
{
    public PlateIndex plateIndex;
    [SerializeField] Animator anim;
    public Transform pointStay;
    public Cake currentCake;
    public WayPointTemp wayPoint;
    public int currentPiecesCountGet;
    public List<IDInfor> idInfors = new(); 
    [SerializeField] Transform trsMove;
    [SerializeField] Vector3 pointMoveUp;
    [SerializeField] Vector3 pointMoveDown;
    public int currentPieceSame;
    public int currentSpace;
    [SerializeField] bool tutPlate;
    public bool actived;
    [SerializeField] GameObject ring;

    private void Awake()
    {
        //pointMoveUp = trsMove.position;
        //pointMoveDown = trsMove.position;
        pointMoveUp.y += .3f;
    }
    public void ActivePlate()
    {
        actived = tutPlate || ProfileManager.Instance.playerData.playerResourseSave.currentLevel > 0;
    }
    public void SetPlateIndex(int x, int y) {
        plateIndex = new PlateIndex(x, y);
    }
    public PlateIndex GetPlateIndex() { return plateIndex; }

    public void SetCurrentCake(Cake cake) {
        currentCake = cake;
        cakeTemp = cake;
        for (int i = pointStay.childCount - 1; i >= 0; i--)
        {
            if (!pointStay.GetChild(i).gameObject.activeSelf)
                Destroy(pointStay.GetChild(i).gameObject);
        }
    }

    public void CakeDone() { currentCake = null; }

    public void Active() {
        ring.SetActive(true);
        //anim.SetBool("Active", true);
        if (trsMove != null)
            trsMove.DOMove(pointMoveUp, .15f);
    }

    public void ActiveByItem() {
        anim.SetBool("Active", true);
    }

    public void Deactive()
    {
        ring.SetActive(false);
        //anim.SetBool("Active", false);
        if (trsMove != null)
            trsMove.DOMove(pointMoveDown, 1f).SetEase(Ease.InOutCubic);
    }

    public void DeActiveByItem()
    { 
        //anim.SetBool("Active", false);
    }
    
    public int CalculatePoint(int cakeID)
    {
        if (currentCake == null) return 0;
        int point = 0;
        for (int i = 0; i < currentCake.pieces.Count; i++)
        {
            if (currentCake.pieces[i].cakeID == cakeID)
            {
                point++;
            }
            else {
                point--;
            }
        }
        point += GetFreeSpace();
        return point;

    }

    public int GetFreeSpace() {
        if (currentCake == null) return 0;
        return 6 - currentCake.pieces.Count;
    }

    public bool CheckModeDone(int cakeID) {
        if (currentCake == null) return true;
        return currentCake.CheckMoveDone(cakeID);
    }

    public bool BestPlateDone(int cakeID, int totalPieceSame) {
        if (currentCake == null) return true;
        return currentCake.CheckBestCakeDone(cakeID, totalPieceSame);
    }

    public int GetCurrentPieceSame(int cakeID)
    {
        if (currentCake == null) return 0;
        return currentCake.GetCurrentPiecesSame(cakeID);
    }

    public void AddPiece(Piece piece)
    {
        currentCake.AddPieces(piece);
    }
    public void CheckNullPieces() {
        if (currentCake == null)
            return;
    }
    public void MoveDoneOfCake()
    {
        if (currentCake == null)
            return;
    }

    public void ClearCake() {
        if (currentCake == null)
            return;
        currentCake.transform.DOScale(Vector3.zero, .5f).OnComplete(() => {
            currentCake?.gameObject.SetActive(false);
            ProfileManager.Instance.playerData.cakeSaveData.RemoveCake(plateIndex);
            currentCake = null;
        });
    }

    public void ClearCakeByBomb() {
        if (currentCake != null)
        {
            DestroyCake();
            ProfileManager.Instance.playerData.cakeSaveData.RemoveCake(plateIndex);
            currentCake = null;
        }
    }
    Cake cakeTemp = null;
    void DestroyCake() {
        if (cakeTemp != currentCake && currentCake != null)
            return;
        cakeTemp?.ClearAnimation();
        Destroy(cakeTemp?.gameObject);
    }

    Transform pointTrashBin;
    Vector3 vectorRotate = new Vector3(0, -35, -25);
    public void ClearCakeFromItem() {
        pointTrashBin = GameManager.Instance.cakeManager.trashBin;
        currentCake.transform.DORotate(vectorRotate, .25f);
        currentCake.transform.DOJump(pointTrashBin.position, 3, 1, .25f).SetEase(Ease.OutCubic).OnComplete(() => {
            if (currentCake != null)
            {
                ClearCakeByBomb();
                currentCake = null;
            }
        });
        
    }

    public bool CheckCakeIsDone(int cakeID) {
        if (currentCake == null) return true;
        return currentCake.CheckCakeIsDone(cakeID);
    }

    public void DoneCake()
    {
        if (currentCake != null)
        {
            currentCake.DoneCakeMode();
            ProfileManager.Instance.playerData.cakeSaveData.RemoveCake(plateIndex);
            //Destroy(currentCake.gameObject);
            currentCake = null;
        }
    }
    public Piece GetPieceMove(int cakeID) {
        if (currentCake == null)
            return null;
        return currentCake.GetPieceMove(cakeID);
    }

    public void SetCountPieces(int countpieces) { currentPiecesCountGet = countpieces; }
    public void ResetPiecesCount() { currentPiecesCountGet = 0; }

    public int GetPieceFree()
    {
        return currentCake.GetPieceFree();
    }

    public void SetCurrentIDInfors() {
        idInfors = currentCake.GetIDInfor();
    }

    public bool CheckIsNull()
    {
        if (currentCake == null) {
            //Debug.LogWarning("plate " + plateIndex.indexX + " " + plateIndex.indexY + " cake null!");
            return true;
        }
        if (currentCake.cakeDone) {
            //Debug.LogWarning("plate " + plateIndex.indexX + " " + plateIndex.indexY + " cake done!");
            return true;
        }
        if (currentCake.pieces.Count == 0) {
            //Debug.LogWarning("plate " + plateIndex.indexX + " " + plateIndex.indexY + " cake pieces count = 0!");
            return true;
        }
       // Debug.LogWarning("plate " + plateIndex.indexX + " " + plateIndex.indexY + " not null!");
        return false;

    }

    public void AnimationLoose()
    {
        anim.SetBool("Alert", true);
    }

    public void AnimationLooseOut()
    {
        anim.SetBool("Alert", false);
    }
}
[System.Serializable]
public class PlateIndex {
    public int indexX;
    public int indexY;
    public PlateIndex(int x, int y) {  indexX = x; indexY = y;}
    public PlateIndex(PlateIndex plateIndex) { indexX = plateIndex.indexX; indexY = plateIndex.indexY; }
}
[System.Serializable]
public class WayPointTemp {
    public Plate nextPlate;
    public bool setDone;
}

[System.Serializable]
public class IDInfor
{
    public int ID;
    public int count;
}
