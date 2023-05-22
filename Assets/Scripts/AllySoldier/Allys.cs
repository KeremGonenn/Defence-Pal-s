using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allys : MonoBehaviour
{
    public List<GameObject> soldierPrefabs; // Soldier prefablar�n� atamak i�in Inspector'dan s�r�kleyip b�rak�n
    public List<Transform> spawnNoktalari; // Spawn noktalar�n� atamak i�in Inspector'dan s�r�kleyip b�rak�n
    public int maksimumSpawnSayisi = 3; // Maksimum spawn say�s�n� ayarlay�n

    private int askerSayaci = 0; // Asker sayac�

    public void OnSpawnButtonPressed()
    {
        if (askerSayaci >= maksimumSpawnSayisi)
        {
            return; // Maksimum spawn say�s�na ula��ld���nda spawn i�lemini durdur
        }

        int spawnIndex = askerSayaci % spawnNoktalari.Count; // Spawn noktas� indeksini d�ng� i�inde de�i�tir
        int prefabIndex = askerSayaci % soldierPrefabs.Count; // Soldier prefab� indeksini d�ng� i�inde de�i�tir

        GameObject newAsker = Instantiate(soldierPrefabs[prefabIndex], spawnNoktalari[spawnIndex].position, spawnNoktalari[spawnIndex].rotation);

        // Yeni asker nesnesi ile ilgili istedi�iniz ayarlamalar� yapabilirsiniz
        // �rne�in, askerin hedefini veya hareketini ayarlayabilirsiniz

        askerSayaci++; // Sadece ba�ar�l� spawn i�lemi sonras�nda asker sayac�n� artt�r
    }
}
