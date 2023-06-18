using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Allys : MonoBehaviour
{
    public List<SoldierAttack> Soldiers;
    public List<GameObject> soldierPrefabs; // Soldier prefablarýný atamak için Inspector'dan sürükleyip býrakýn
    public List<Transform> spawnNoktalari; // Spawn noktalarýný atamak için Inspector'dan sürükleyip býrakýn
    public int maksimumSpawnSayisi = 3; // Maksimum spawn sayýsýný ayarlayýn

    private int soldierCount = 0; // Asker sayacý

    [SerializeField] private float _soldierCost;
    [SerializeField] private float _soldierCostMultiple;
    [SerializeField] private TMP_Text _soldierCostText;

    public static Allys Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Soldiers = new List<SoldierAttack>();
    }

    public void SpawnSoldiers()
    {
        if (soldierCount >= maksimumSpawnSayisi)
        {
            return; // Maksimum spawn sayýsýna ulaþýldýðýnda spawn iþlemini durdur
        }

        if(GoldManager.Instance.gold < _soldierCost)
        {
            return;
        }

        int spawnIndex = soldierCount % spawnNoktalari.Count; // Spawn noktasý indeksini döngü içinde deðiþtir
        int prefabIndex = soldierCount % soldierPrefabs.Count; // Soldier prefabý indeksini döngü içinde deðiþtir

        GameObject newSoldier = Instantiate(soldierPrefabs[prefabIndex], spawnNoktalari[spawnIndex].position, spawnNoktalari[spawnIndex].rotation);

        // Yeni asker nesnesi ile ilgili istediðiniz ayarlamalarý yapabilirsiniz
        // Örneðin, askerin hedefini veya hareketini ayarlayabilirsiniz
        Soldiers.Add(newSoldier.GetComponent<SoldierAttack>());

        soldierCount++; // Sadece baþarýlý spawn iþlemi sonrasýnda asker sayacýný arttýr

        TakeSoldier();
    }

    private void TakeSoldier()
    {

            GoldManager.Instance.gold -= (int)_soldierCost;
            _soldierCost *= _soldierCostMultiple;
            _soldierCostText.text = _soldierCost.ToString();
            GoldManager.Instance.UpdateGoldUI();


    }
}
