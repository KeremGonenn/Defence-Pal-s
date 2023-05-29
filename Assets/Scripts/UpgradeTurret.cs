using System.Collections;
using System.Collections.Generic;
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

    public void UpgradeDamage()
    {
        if (GoldManager.Instance.gold >= damageUpgradeCost)
        {
            towerBullet.SetDamage(damage);
            cannonBall.SetDamage(damage);
            GoldManager.Instance.gold -= damageUpgradeCost;

            damageUpgradeCost *= damageUpgradeCostMultiple;
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

            speedUpgradeCost *= speedUpgradeCostMultiple;
        }
    }

    public void UpgradeRange()
    {
        if (GoldManager.Instance.gold >= rangeUpgradeCost)
        {
            enemyDetector.transform.localScale *= range;
            GoldManager.Instance.gold -= rangeUpgradeCost;

            rangeUpgradeCost *= rangeUpgradeCostMultiple;
        }
    }
}
