using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Serializable]
    public class Upgrade
    {
        public int level;             //Level of upgrade.
        public int MaxLevel = 5;     // Max level of upgrade;
        public int price;             //Price on store
        public int scalePrice;
        public float value;
        public float scaleValue;      //how much increases when an upgrade happens.

        public Upgrade(
            int level,             //Level of upgrade.
            int MaxLevel,     // Max level of upgrade;
            int price,            //Price on store
            int scalePrice,
            float value,
            float scaleValue)
        {
            this.level = level;
            this.MaxLevel = MaxLevel;
            this.price = price;
            this.scalePrice = scalePrice;
            this.value = value;
            this.scaleValue = scaleValue;
        }

        public void LevelUp()
        {
            level++;
            if (level > MaxLevel)
            {
                level = MaxLevel;
                return;
            }

            price += scalePrice;
            TotalCoins.Instance.totalCoins -= price;
            value += scaleValue;
        }
    }

    public Upgrade meleeDamage = new Upgrade(0, 5, 15, 15, 2, 2);
    public Upgrade rangeDamage = new Upgrade(0, 5, 20, 25, 1, 1);
    public Upgrade rangeROF = new Upgrade(0, 5, 15, 30, 1, 1);
    public Upgrade Health = new Upgrade(0, 5, 15, 15, 10, 2);
    public Upgrade Speed = new Upgrade(0, 5, 15, 35, 4, 1);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void PurchaseUpgrade(UpgradeTypes type)
    {
        switch (type)
        {
            case UpgradeTypes.melee:
                if (TotalCoins.Instance.totalCoins >= meleeDamage.price)
                {
                    meleeDamage.LevelUp();
                }
                break;
            case UpgradeTypes.range_damage:
                if (TotalCoins.Instance.totalCoins >= rangeDamage.price)
                {
                    rangeDamage.LevelUp();
                }
                break;
            case UpgradeTypes.range_ROF:
                if (TotalCoins.Instance.totalCoins >= rangeROF.price)
                {
                    rangeROF.LevelUp();
                }
                break;
            case UpgradeTypes.health:
                if (TotalCoins.Instance.totalCoins >= Health.price)
                {
                    Health.LevelUp();
                }
                break;
            case UpgradeTypes.speed:
                if (TotalCoins.Instance.totalCoins >= Speed.price)
                {
                    Speed.LevelUp();
                }
                break;
        }
    }

}
public enum UpgradeTypes
{
    melee,
    range_damage,
    range_ROF,
    health,
    speed
}
