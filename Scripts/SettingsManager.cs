using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SettingsManager : MonoBehaviour
{
    public VerticalLayoutGroup layout;
    private float spacing = 0.01f;

    public Color loseColor;
    public Color loseSelectedColor;
    public Color winColor;
    public Color winSelectedColor;

    private string latest = "dragontail-10.25.1";
    private string settingsPath;
    private Settings settings;

    public static SettingsManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SettingsManager found!");
            return;
        }
        instance = this;
        settingsPath = string.Join("/", Application.persistentDataPath, "settings.txt");
        StartCoroutine(CheckForUpdates());
        Load();
        UpdateContentSize();
    }

    public void UpdateContentSize()
    {
        //Small hack to update the size of the layouts when elements are added/removed.
        layout.spacing += spacing;
        spacing *= -1f;
    }

    IEnumerator CheckForUpdates()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URLs.GetVersions()))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                print("Error: " + www.error);
            else
            {
                List<string> versions = JsonConvert.DeserializeObject<List<string>>(www.downloadHandler.text);
                if ("dragontail-" + versions[0] != latest)
                {
                    print("DataDragon not up to date!");
                }
            }
        }
        StartCoroutine(DownloadExtract());
    }

    IEnumerator DownloadExtract()
    {
        string downloadPath = string.Join("/", Application.persistentDataPath, latest + ".zip");
        bool toExtract = false;
        if (!File.Exists(downloadPath))
        {
            using (UnityWebRequest www = UnityWebRequest.Get(string.Join("/", "http://gimmearecipe.000webhostapp.com", latest + ".zip")))
            {
                DownloadHandlerFile dhf = new DownloadHandlerFile(downloadPath)
                {
                    removeFileOnAbort = true
                };
                www.downloadHandler = dhf;
                print("Started download at " + Time.time);
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                    print("Error: " + www.error);
                else
                {
                    print("Finished download at " + Time.time);
                    toExtract = true;
                }
            }
        }
        if (!Directory.Exists(string.Join("/", Application.persistentDataPath, latest)) || toExtract)
        {
            print("Started extraction at " + Time.time);
            ZipFile.ExtractToDirectory(downloadPath, Application.persistentDataPath);
            print("Finished extraction at " + Time.time);
        }
    }

    void Load()
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            settings = JsonUtility.FromJson<Settings>(json);
        }
        else
        {
            settings = new Settings();
            Save();
        }
    }

    void Save()
    {
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(settingsPath, json);
    }

    public string GetRegion()
    {
        return settings.region;
    }

    public string GetLanguage()
    {
        return settings.language;
    }
}

public class Settings
{
    public string region = "euw1";
    public string language = "en_US";
}