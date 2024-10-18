using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Object = System.Object;

public class MoneyManager : MonoBehaviour
{
    [Header("General money info")]
    [SerializeField] private float startLevelMoney;
    [SerializeField] private float moneyAmount;
    [SerializeField] private int increaseMoneyPerBlock = 3;
    
    [Header("Upgrade money texts")]
    [SerializeField] private TextMeshProUGUI countMoney;
    [SerializeField] private TextMeshProUGUI speedUpgradeCost;
    [SerializeField] private TextMeshProUGUI workersUpgradeCost;
    [SerializeField] private TextMeshProUGUI strenghtUpgradeCost;

    [SerializeField] private GameObject coinImage;
    
    private void Start()
    {
        moneyAmount = startLevelMoney;
        speedUpgradeCost.text = 5.ToString();
        workersUpgradeCost.text = 10.ToString();
        strenghtUpgradeCost.text = 200.ToString();
        EarnMoney();
    }

    private void Update()
    {
        countMoney.text = GetMoney().ToString(CultureInfo.InvariantCulture);
    }

    public void EarnMoney()
    {
        moneyAmount += increaseMoneyPerBlock;
        if (!earnMoney.IsActive())
        {
            earnMoney = coinImage.transform.DOScale(1.3f, 0.1f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo);
        }
    }

    public float GetMoney()
    {
        return moneyAmount;
    }

    public bool CanUpgradeStrength()
    {
        if (priceStrenght <= moneyAmount)
        {
            moneyAmount -= priceStrenght;
            priceStrenght += tempStrength;
            tempStrength += 200;
            strenghtUpgradeCost.text = priceStrenght.ToString();
            return true;
        }

        return false;
    }

    public bool CanUpgradeCountWorkers()
    {
        if (priceWorkers <= moneyAmount)
        {
            moneyAmount -= priceWorkers;
            priceWorkers += tempWokers;
            tempWokers += 10;
            workersUpgradeCost.text = priceWorkers.ToString();
            return true;
        }

        return false;
    }
    
    public bool CanUpgradeSpeed()
    {
        if (priceSpeed <= moneyAmount)
        {
            moneyAmount -= priceSpeed;
            priceSpeed += tempSpeed;
            tempSpeed += 5;
            speedUpgradeCost.text = priceSpeed.ToString();
            return true;
        }

        return false;
    }

    public bool CanWorkersBeUpgraded()
    {
        return priceWorkers <= moneyAmount;
    }
    
    public bool CanSpeedBeUpgraded()
    {
        return priceSpeed <= moneyAmount;
    }
    
    public bool CanStrengthBeUpgraded()
    {
        return priceStrenght <= moneyAmount;
    }
    

    private int priceSpeed = 5;
    private int priceStrenght = 200;
    private int priceWorkers = 10;

    private int tempSpeed = 15;
    private int tempStrength = 150;
    private int tempWokers = 20;

    private Tweener earnMoney;
}
