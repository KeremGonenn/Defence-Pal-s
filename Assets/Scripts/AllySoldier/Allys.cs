using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Allys : MonoBehaviour
{
    public List<SoldierAttack> Soldiers;
    public List<GameObject> soldierPrefabs; // Soldier prefablar�n� atamak i�in Inspector'dan s�r�kleyip b�rak�n
    public List<Transform> spawnNoktalari; // Spawn noktalar�n� atamak i�in Inspector'dan s�r�kleyip b�rak�n
    public int maksimumSpawnSayisi = 3; // Maksimum spawn say�s�n� ayarlay�n

    private int soldierCount = 0; // Asker sayac�

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
            return; // Maksimum spawn say�s�na ula��ld���nda spawn i�lemini durdur
        }

        if(GoldManager.Instance.gold < _soldierCost)
        {
            return;
        }

        int spawnIndex = soldierCount % spawnNoktalari.Count; // Spawn noktas� indeksini d�ng� i�inde de�i�tir
        int prefabIndex = soldierCount % soldierPrefabs.Count; // Soldier prefab� indeksini d�ng� i�inde de�i�tir

        GameObject newSoldier = Instantiate(soldierPrefabs[prefabIndex], spawnNoktalari[spawnIndex].position, spawnNoktalari[spawnIndex].rotation);

        // Yeni asker nesnesi ile ilgili istedi�iniz ayarlamalar� yapabilirsiniz
        // �rne�in, askerin hedefini veya hareketini ayarlayabilirsiniz
        Soldiers.Add(newSoldier.GetComponent<SoldierAttack>());

        soldierCount++; // Sadece ba�ar�l� spawn i�lemi sonras�nda asker sayac�n� artt�r

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
