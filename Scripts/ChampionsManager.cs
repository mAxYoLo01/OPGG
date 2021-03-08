using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataDragon;
using Newtonsoft.Json;
using System;
using System.IO;

public class ChampionsManager : MonoBehaviour
{
    public Dictionary<int, Champion> championsList = new Dictionary<int, Champion>();

    public static ChampionsManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ChampionsManager found!");
            return;
        }
        instance = this;
        SetupChampions();
    }

    
    void SetupChampions()
    {
        ChampionList champList = JsonConvert.DeserializeObject<ChampionList>(File.ReadAllText(URLs.GetChampions()));
        foreach (KeyValuePair<string, Champion> keyValue in champList.champions)
        {
            championsList.Add(Int16.Parse(keyValue.Value.key), keyValue.Value);
        }
    }

    public Texture2D GetChampionIcon(string id)
    {
        byte[] byteArray = File.ReadAllBytes(URLs.GetChampionIcon(id));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(byteArray);
        return texture;
    }

    public Texture2D GetItemIcon(int id)
    {
        if (id == 0)
            return null;
        byte[] byteArray = File.ReadAllBytes(URLs.GetItemIcon(id));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(byteArray);
        return texture;
    }

    public Texture2D GetMapImage(int id)
    {
        byte[] byteArray = File.ReadAllBytes(URLs.GetMap(id));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(byteArray);
        return texture;
    }
}
