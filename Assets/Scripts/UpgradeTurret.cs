using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public int gold;
}

public class UpgradeTurret : MonoBehaviour
{
    [SerializeField] private TowerBullet towerBullet;
    [SerializeField] private CannonBall cannonBall;
    [SerializeField] List<NPCTower> npcTowers;
    [SerializeField] private GameObject enemyDetector;


    public Tower tower; // Yükseltme yapýlacak kule
    public Text damageText; // Hasar metni
    public Text speedText; // Atak hýzý metni
    public Text rangeText; // Menzil metni

    public int damage;
    public float attackSpeed;
    public float range;

    [SerializeField] private int damageUpgradeCost = 100;
    [SerializeField] private int speedUpgradeCost = 100;
    [SerializeField] private int rangeUpgradeCost = 100;

    [SerializeField] private int damageUpgradeCostMultiple = 100;
    [SerializeField] private int speedUpgradeCostMultiple = 100;
    [SerializeField] private int rangeUpgradeCostMultiple = 100;

    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _attackSpeedText;
    [SerializeField] private TMP_Text _rangeText;

    private void Start()
    {
        _damageText.text = damageUpgradeCost.ToString() + " $";
        _attackSpeedText.text = speedUpgradeCost.ToString()+" $";
        _rangeText.text = rangeUpgradeCost.ToString() + " $";

    }

    public void UpgradeDamage()
    {
        if (GoldManager.Instance.gold >= damageUpgradeCost)
        {
            towerBullet.SetDamage(damage);
            cannonBall.SetDamage(damage);
            GoldManager.Instance.gold -= damageUpgradeCost;
            GoldManager.Instance.UpdateGoldUI();

            damageUpgradeCost *= damageUpgradeCostMultiple;
            _damageText.text = damageUpgradeCost.ToString() +" $";
        }
    }

    public void UpgradeAttackSpeed()
    {
        if (GoldManager.Instance.gold >= speedUpgradeCost)
        {
            foreach (var item in npcTowers)
            {
                item.ReducecAttackTime(attackSpeed);
            }
            GoldManager.Instance.gold -= speedUpgradeCost;
            GoldManager.Instance.UpdateGoldUI();

            speedUpgradeCost *= speedUpgradeCostMultiple;
            _attackSpeedText.text = speedUpgradeCost.ToString() + " $";
        }
    }

    public void UpgradeRange()
    {
        if (GoldManager.Instance.gold >= rangeUpgradeCost)
        {
            enemyDetector.transform.localScale *= range;
            GoldManager.Instance.gold -= rangeUpgradeCost;
            GoldManager.Instance.UpdateGoldUI();

            rangeUpgradeCost *= rangeUpgradeCostMultiple;
            _rangeText.text = rangeUpgradeCost.ToString() + " $";
        }
    }
}
