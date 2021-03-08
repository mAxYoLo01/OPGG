using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLs
{
    static string api_key = "RGAPI-206b90b1-8a5e-40d9-bccb-35e2e00c0e2d";

    #region SummonerV4
    public static string GetSummonerFromName(string name)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{name}?api_key={api_key}";
    }

    public static string GetSummonerFromId(string id)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/summoner/v4/summoners/{id}?api_key={api_key}";
    }

    public static string GetSummonerFromAccountId(string accountId)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/summoner/v4/summoners/by-account/{accountId}?api_key={api_key}";
    }

    public static string GetSummonerFromPuuid(string puuid)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/summoner/v4/summoners/by-puuid/{puuid}?api_key={api_key}";
    }
    #endregion

    #region DataDragon
    public static string GetVersions()
    {
        return $"https://ddragon.leagueoflegends.com/api/versions.json";
    }

    public static string GetProfileIcon(int profileIconId)
    {
        return $"http://ddragon.leagueoflegends.com/cdn/10.25.1/img/profileicon/{profileIconId}.png";
    }

    public static string GetChampions()
    {
        return $"{Application.persistentDataPath}/dragontail-10.25.1/10.25.1/data/{SettingsManager.instance.GetLanguage()}/champion.json";
    }

    public static string GetChampionIcon(string id)
    {
        return $"{Application.persistentDataPath}/dragontail-10.25.1/10.25.1/img/champion/{id}.png";
    }

    public static string GetItemIcon(int id)
    {
        return $"{Application.persistentDataPath}/dragontail-10.25.1/10.25.1/img/item/{id}.png";
    }

    public static string GetMap(int id)
    {
        return $"{Application.persistentDataPath}/dragontail-10.25.1/10.25.1/img/map/map{id}.png";
    }
    #endregion

    #region MatchV4
    public static string GetMatchlist(string accountId, int endIndex, int beginIndex)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/match/v4/matchlists/by-account/{accountId}?endIndex={endIndex}&beginIndex={beginIndex}&api_key={api_key}";
    }

    public static string GetMatch(long gameId)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/match/v4/matches/{gameId}?api_key={api_key}";
    }

    public static string GetMatchTimeline(long gameId)
    {
        return $"https://{SettingsManager.instance.GetRegion()}.api.riotgames.com/lol/match/v4/timelines/by-match/{gameId}?api_key={api_key}";
    }
    #endregion
}
