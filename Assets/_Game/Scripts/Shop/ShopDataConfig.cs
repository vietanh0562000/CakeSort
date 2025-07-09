using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopDataConfig", menuName = "ScriptableObject/ShopDataConfig")]
public class ShopDataConfig : ScriptableObject
{
    public List<ShopPack> shopPacks = new List<ShopPack>();

    public ShopPack GetShopPack(OfferID packageId)
    {
        for (int i = 0; i < shopPacks.Count; i++)
        {
            if (shopPacks[i].packageId == packageId)
                return shopPacks[i];
        }
        return null;
    }

    public ShopPack GetShopPack(string packageName)
    {
        for (int i = 0; i < shopPacks.Count; i++)
        {
            if (shopPacks[i].packageId.ToString() == packageName)
                return shopPacks[i];
        }
        return null;
    }
}

[System.Serializable]
public class ShopPack
{
    public OfferID packageId;
    public List<ItemData> rewards = new();
    public float defaultPrice;
}

public enum OfferID
{
    None = 0,
    com_mergecake_pack1 = 1,
    com_mergecake_pack2 = 2,
    com_mergecake_piggy_pack = 5,
    com_mergecake_pack_hammer = 10,
    com_mergecake_pack_fillup = 20,
    com_mergecake_pack_reroll = 30,
    com_mergecake_pack_money1 = 40,
    com_mergecake_pack_money2 = 41,
    com_mergecake_pack_money3 = 42,
    com_mergecake_pack_money4 = 43,
    com_mergecake_pack_money5 = 44,
    com_mergecake_pack_money6 = 45,
}