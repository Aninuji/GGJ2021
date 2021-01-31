using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeTypes upgrade;

    public Button button;
    public Text price;

    public Text level;
    // Start is called before the first frame update
    void Start()
    {
        TotalCoins.Instance.coinChangeEvent += UpdateButton;
    }

    void OnDestroy()
    {
        TotalCoins.Instance.coinChangeEvent -= UpdateButton;
    }

    public void UpdateButton(int coins)
    {
        Debug.Log("Updating");
        switch (upgrade)
        {
            case UpgradeTypes.melee:
                price.text = UpgradeManager.Instance.meleeDamage.price + "$";
                level.text = "lvl" + UpgradeManager.Instance.meleeDamage.level.ToString();
                if (TotalCoins.Instance.totalCoins <= UpgradeManager.Instance.meleeDamage.price
                || UpgradeManager.Instance.meleeDamage.level == UpgradeManager.Instance.meleeDamage.MaxLevel)
                {
                    button.interactable = false;
                }
                break;
            case UpgradeTypes.range_damage:
                price.text = UpgradeManager.Instance.rangeDamage.price + "$";
                level.text = "lvl" + UpgradeManager.Instance.rangeDamage.level.ToString();
                if (TotalCoins.Instance.totalCoins <= UpgradeManager.Instance.rangeDamage.price
                || UpgradeManager.Instance.rangeDamage.level == UpgradeManager.Instance.rangeDamage.MaxLevel)
                {
                    button.interactable = false;
                }
                break;
            case UpgradeTypes.range_ROF:
                price.text = UpgradeManager.Instance.rangeROF.price + "$";
                level.text = "lvl" + UpgradeManager.Instance.rangeROF.level.ToString();
                if (TotalCoins.Instance.totalCoins <= UpgradeManager.Instance.rangeROF.price
                || UpgradeManager.Instance.rangeROF.level == UpgradeManager.Instance.rangeROF.MaxLevel)
                {
                    button.interactable = false;
                }
                break;
            case UpgradeTypes.health:
                price.text = UpgradeManager.Instance.Health.price + "$";
                level.text = "lvl" + UpgradeManager.Instance.Health.level.ToString();
                if (TotalCoins.Instance.totalCoins <= UpgradeManager.Instance.Health.price
                || UpgradeManager.Instance.Health.level == UpgradeManager.Instance.Health.MaxLevel)
                {
                    button.interactable = false;
                }
                break;
            case UpgradeTypes.speed:
                price.text = UpgradeManager.Instance.Speed.price + "$";
                level.text = "lvl" + UpgradeManager.Instance.Speed.level.ToString();
                if (TotalCoins.Instance.totalCoins <= UpgradeManager.Instance.Speed.price
                || UpgradeManager.Instance.Speed.level == UpgradeManager.Instance.Speed.MaxLevel)
                {
                    button.interactable = false;
                }
                break;
        }
    }

    public void PurchaseUpgrade()
    {
        UpgradeManager.Instance.PurchaseUpgrade(upgrade);
    }
}
