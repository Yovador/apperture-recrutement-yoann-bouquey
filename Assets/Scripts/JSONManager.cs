using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;




// Script that manage all link with "galery.json"

[System.Serializable]
public class CardData
{
    public string name;
    public string categorie;
    public string asset_name;
}

[System.Serializable]
public class Galery
{
    public List<CardData> item_list;
}

// class that communicate with the json
public class JSONManager
{

    public static Galery ReadJson()
    {
        Galery newGalery = new Galery();
        string path = Path.Combine(Application.streamingAssetsPath, "saved_galery/galery.json");
        if (File.Exists(path))
        {
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                string JSON = "";
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    JSON += temp.GetString(b);
                }
                newGalery = JsonUtility.FromJson<Galery>(JSON);
            }
        }
        return newGalery;

    }

}
