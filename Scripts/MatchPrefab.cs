using DataDragon;
using MatchV4;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchPrefab : MonoBehaviour
{
    public Match match;
    public MatchTimeline timeline;
    public Text winLoseText;
    public Text championNameText;
    public RawImage championImage;
    public GameObject popupStats;
    public Transform bluePlayers;
    public Transform redPlayers;
    public Image background;
    public List<GameObject> tabs;

    private GameObject lastOpened;
    private int playerParticipantId;

    public void SetupUI(string accountId)
    {
        SetPlayerParticipantId(accountId);

        foreach (Participant participant in match.participants)
        {
            if (playerParticipantId == participant.participantId)
            {
                winLoseText.text = participant.stats.win ? "Win" : "Lose";
                Champion champion = ChampionsManager.instance.championsList[participant.championId];
                championNameText.text = champion.name;
                championImage.texture = ChampionsManager.instance.GetChampionIcon(champion.id);
                background.color = participant.stats.win ? SettingsManager.instance.winSelectedColor : SettingsManager.instance.loseSelectedColor;
                break;
            }
        }
    }

    void SetPlayerParticipantId(string accountId)
    {
        foreach (ParticipantIdentity identity in match.participantIdentities)
        {
            if (identity.player.accountId == accountId)
            {
                playerParticipantId = identity.participantId;
                break;
            }
        }
    }

    public void OpenMore()
    {
        if (lastOpened == null)
        {
            StartCoroutine(SetTimeline());
            lastOpened = tabs[0];
            foreach (Participant participant in match.participants)
            {
                Transform team = (participant.teamId == 100) ? bluePlayers : redPlayers;
                int shift = (participant.teamId == 100) ? -1 : -6;
                PlayerInMatchPrefab playerInMatch = team.GetChild(participant.participantId + shift).GetComponent<PlayerInMatchPrefab>();
                playerInMatch.participant = participant;
                foreach (ParticipantIdentity participantIdentity in match.participantIdentities)
                {
                    if (participantIdentity.participantId == participant.participantId)
                    {
                        playerInMatch.participantIdentity = participantIdentity;
                        break;
                    }
                }
                playerInMatch.SetupUI(playerParticipantId);
            }
        }
        if (popupStats.activeSelf)
        {
            popupStats.SetActive(false);
        } else
        {
            popupStats.SetActive(true);
            OpenTab(lastOpened);
        }
        SettingsManager.instance.UpdateContentSize();
    }

    IEnumerator SetTimeline()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URLs.GetMatchTimeline(match.gameId)))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                print("Error: " + www.error);
            else
            {
                timeline = JsonConvert.DeserializeObject<MatchTimeline>(www.downloadHandler.text);
                MapGenerator mapGen = tabs[1].GetComponent<MapGenerator>();
                mapGen.match = match;
                mapGen.timeline = timeline;
                mapGen.SetupUI(playerParticipantId);
            }
        }
    }

    public void OpenTab(GameObject go)
    {
        foreach (GameObject tmp in tabs)
        {
            tmp.SetActive(false);
        }
        go.SetActive(true);
        lastOpened = go;
    }
}
