using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchV4;
using UnityEngine.UI;
using DataDragon;

public class PlayerInMatchPrefab : MonoBehaviour
{
    public Participant participant;
    public ParticipantIdentity participantIdentity;
    public RawImage championImage;
    public Text summonerName;
    public Text KDA;
    public Text K_D_A;
    public Image background;
    public Transform items;

    public void SetupUI(int participantId)
    {
        gameObject.name = participantIdentity.player.summonerName;
        summonerName.text = participantIdentity.player.summonerName;
        Champion champion = ChampionsManager.instance.championsList[participant.championId];
        championImage.texture = ChampionsManager.instance.GetChampionIcon(champion.id);
        K_D_A.text = $"{participant.stats.kills}/{participant.stats.deaths}/{participant.stats.assists}";
        float kda = ((float)participant.stats.kills + (float)participant.stats.assists) / (float)participant.stats.deaths;
        KDA.text = kda.ToString("0.00").Replace(',', '.');
        KDA.color = GetKDAColor(kda);
        if (participant.participantId == participantId)
            background.color = participant.stats.win ? SettingsManager.instance.winSelectedColor : SettingsManager.instance.loseSelectedColor;
        else
            background.color = participant.stats.win ? SettingsManager.instance.winColor : SettingsManager.instance.loseColor;
        items.GetChild(0).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item0);
        items.GetChild(1).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item1);
        items.GetChild(2).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item2);
        items.GetChild(3).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item3);
        items.GetChild(4).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item4);
        items.GetChild(5).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item5);
        items.GetChild(6).GetComponent<RawImage>().texture = ChampionsManager.instance.GetItemIcon(participant.stats.item6);
    }

    public Color GetKDAColor(float kda)
    {
        if (kda >= 10)
            return new Color(0.5f, 0f, 0.5f);
        if (kda >= 5)
            return new Color(1f, 0.65f, 0f);
        if (kda >= 3)
            return new Color(0f, 0.5f, 0f);
        if (kda >= 1)
            return new Color(0.5f, 0.5f, 0.5f);
        return Color.red;
    }
}
