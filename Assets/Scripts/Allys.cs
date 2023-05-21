using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allys : MonoBehaviour
{
    public List<GameObject> soldierPrefabs; // Soldier prefablarýný atamak için Inspector'dan sürükleyip býrakýn
    public List<Transform> spawnNoktalari; // Spawn noktalarýný atamak için Inspector'dan sürükleyip býrakýn
    public int maksimumSpawnSayisi = 3; // Maksimum spawn sayýsýný ayarlayýn

    private int askerSayaci = 0; // Asker sayacý

    public void OnSpawnButtonPressed()
    {
        if (askerSayaci >= maksimumSpawnSayisi)
        {
            return; // Maksimum spawn sayýsýna ulaþýldýðýnda spawn iþlemini durdur
        }

        int spawnIndex = askerSayaci % spawnNoktalari.Count; // Spawn noktasý indeksini döngü içinde deðiþtir
        int prefabIndex = askerSayaci % soldierPrefabs.Count; // Soldier prefabý indeksini döngü içinde deðiþtir

        GameObject newAsker = Instantiate(soldierPrefabs[prefabIndex], spawnNoktalari[spawnIndex].position, spawnNoktalari[spawnIndex].rotation);

        // Yeni asker nesnesi ile ilgili istediðiniz ayarlamalarý yapabilirsiniz
        // Örneðin, askerin hedefini veya hareketini ayarlayabilirsiniz

        askerSayaci++; // Sadece baþarýlý spawn iþlemi sonrasýnda asker sayacýný arttýr
    }
}
