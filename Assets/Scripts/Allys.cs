using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allys : MonoBehaviour
{
    public GameObject askerPrefab; // Asker prefabini atamak için Inspector'dan sürükleyip býrakýn
    public Transform spawnNoktasi; // Spawn noktasýný atamak için Inspector'dan sürükleyip býrakýn
    public int maksimumSpawnSayisi = 3; // Maksimum spawn sayýsýný ayarlayýn

    private int askerSayaci = 0; // Asker sayacý

    public void OnSpawnButtonPressed()
    {
        askerSayaci++; // Sayaçý arttýr

        for (int i = 0; i < Mathf.Min(askerSayaci, maksimumSpawnSayisi); i++)
        {
            GameObject newAsker = Instantiate(askerPrefab, spawnNoktasi.position, spawnNoktasi.rotation);

            // Yeni asker nesnesi ile ilgili istediðiniz ayarlamalarý yapabilirsiniz
            // Örneðin, askerin hedefini veya hareketini ayarlayabilirsiniz
        }
    }
}
