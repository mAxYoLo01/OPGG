using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SummonerV4;
using MatchV4;

public class PlayerProfile : MonoBehaviour
{
    public RawImage profileIcon;
    public Text level;
    public Text playerName;
    public InputField searchText;
    public Transform matchlistParent;

    private Summoner currentSummoner;
    private List<Match> currentMatches = new List<Match>();
    private Texture2D currentProfileIconTexture;
    private int currentIndex = 0;

    private void Start()
    {
        searchText.text = "mAxYoLo01";
    }

    IEnumerator SetCurrentVariables()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URLs.GetSummonerFromName(searchText.text)))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                print("Error: " + www.error);
            else
                currentSummoner = JsonConvert.DeserializeObject<Summoner>(www.downloadHandler.text);
        }

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(URLs.GetProfileIcon(currentSummoner.profileIconId)))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                print("Error: " + www.error);
            else
                currentProfileIconTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }

        currentMatches.Clear();
        currentIndex = 0;
        using (UnityWebRequest www = UnityWebRequest.Get(URLs.GetMatchlist(currentSummoner.accountId, currentIndex + 10, currentIndex)))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                print("Error: " + www.error);
            else
            {
                Matchlist currentMatchlist = JsonConvert.DeserializeObject<Matchlist>(www.downloadHandler.text);
                foreach (MatchReference match in currentMatchlist.matches)
                {
                    using (UnityWebRequest wwww = UnityWebRequest.Get(URLs.GetMatch(match.gameId)))
                    {
                        yield return wwww.SendWebRequest();
                        if (wwww.isNetworkError || wwww.isHttpError)
                            print("Error: " + wwww.error);
                        else
                            currentMatches.Add(JsonConvert.DeserializeObject<Match>(wwww.downloadHandler.text));
                    }
                }
            }  
        }
        currentIndex += 10;

        profileIcon.texture = currentProfileIconTexture;
        level.text = currentSummoner.summonerLevel.ToString();
        playerName.text = currentSummoner.name;

        foreach (Transform match in matchlistParent)
        {
            Destroy(match.gameObject);
        }

        List<GameObject> tmp = new List<GameObject>();
        foreach (Match match in currentMatches)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/MatchPrefab"), matchlistParent);
            tmp.Add(go);
            go.name = match.gameId.ToString();
            go.GetComponent<MatchPrefab>().match = match;
            go.GetComponent<MatchPrefab>().SetupUI(currentSummoner.accountId);
        }
        foreach (GameObject go in tmp)
        {
            go.SetActive(true);
        }
    }

    IEnumerator AddMoreMatches()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URLs.GetMatchlist(currentSummoner.accountId, currentIndex + 10, currentIndex)))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                print("Error: " + www.error);
            else
            {
                Matchlist currentMatchlist = JsonConvert.DeserializeObject<Matchlist>(www.downloadHandler.text);
                List<GameObject> tmp = new List<GameObject>();
                foreach (MatchReference match in currentMatchlist.matches)
                {
                    using (UnityWebRequest wwww = UnityWebRequest.Get(URLs.GetMatch(match.gameId)))
                    {
                        yield return wwww.SendWebRequest();
                        if (wwww.isNetworkError || wwww.isHttpError)
                            print("Error: " + wwww.error);
                        else
                        {
                            Match currentMatch = JsonConvert.DeserializeObject<Match>(wwww.downloadHandler.text);
                            currentMatches.Add(currentMatch);
                            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/MatchPrefab"), matchlistParent);
                            tmp.Add(go);
                            go.name = currentMatch.gameId.ToString();
                            go.GetComponent<MatchPrefab>().match = currentMatch;
                            go.GetComponent<MatchPrefab>().SetupUI(currentSummoner.accountId);
                            go.SetActive(false);
                        }
                    }
                }
                foreach (GameObject go in tmp)
                {
                    go.SetActive(true);
                }
            }
        }
        currentIndex += 10;
    }

    public void SearchPlayer()
    {
        StartCoroutine(SetCurrentVariables());
    }

    public void MoreMatches()
    {
        StartCoroutine(AddMoreMatches());
    }
}
