using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allys : MonoBehaviour
{
    public GameObject askerPrefab; // Asker prefabini atamak i�in Inspector'dan s�r�kleyip b�rak�n
    public Transform spawnNoktasi; // Spawn noktas�n� atamak i�in Inspector'dan s�r�kleyip b�rak�n
    public int maksimumSpawnSayisi = 3; // Maksimum spawn say�s�n� ayarlay�n

    private int askerSayaci = 0; // Asker sayac�

    public void OnSpawnButtonPressed()
    {
        askerSayaci++; // Saya�� artt�r

        for (int i = 0; i < Mathf.Min(askerSayaci, maksimumSpawnSayisi); i++)
        {
            GameObject newAsker = Instantiate(askerPrefab, spawnNoktasi.position, spawnNoktasi.rotation);

            // Yeni asker nesnesi ile ilgili istedi�iniz ayarlamalar� yapabilirsiniz
            // �rne�in, askerin hedefini veya hareketini ayarlayabilirsiniz
        }
    }
}
