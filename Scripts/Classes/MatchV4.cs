using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchV4
{
    public class Matchlist
    {
        public int startIndex;
        public int endIndex;
        public List<MatchReference> matches;
    }

    public class MatchReference
    {
        public long gameId;
        public string role;
        public int season;
        public string platformId;
        public int champion;
        public int queue;
        public string lane;
        public long timestamp;
    }

    public class Match
    {
        public long gameId;
        public List<ParticipantIdentity> participantIdentities;
        public int queueId;
        public string gameType;
        public long gameDuration;
        public List<TeamStats> teams;
        public string platformId;
        public long gameCreation;
        public int seasonId;
        public string gameVersion;
        public int mapId;
        public string gameMode;
        public List<Participant> participants;
    }

    public class ParticipantIdentity
    {
        public int participantId;
        public Player player;
    }

    public class Player
    {
        public int profileIcon;
        public string accountId;
        public string currentAccountId;
        public string currentPlatformId;
        public string summonerName;
        public string summonerId;
        public string platformId;
    }

    public class TeamStats
    {
        public int teamId;
        public string win;
    }

    public class Participant
    {
        public int participantId;
        public int championId;
        public int teamId;
        public ParticipantStats stats;
    }

    public class ParticipantStats
    {
        public bool win;
        public int kills;
        public int deaths;
        public int assists;
        public int item0;
        public int item1;
        public int item2;
        public int item3;
        public int item4;
        public int item5;
        public int item6;
    }

    public class MatchTimeline
    {
        public List<MatchFrame> frames;
        public long frameInterval;
    }

    public class MatchFrame
    {
        //public Dictionary<string, MatchParticipantFrame> participantFrames;
        public List<MatchEvent> events;
        public long timestamp;
    }

    public class MatchEvent
    {
        public string laneType;
        public int skillSlot;
        public string ascendedType;
        public int creatorId;
        public int afterId;
        public string eventType;
        public string type;
        public string levelUpType;
        public string wardType;
        public int participantId;
        public string towerType;
        public int itemId;
        public int beforeId;
        public string pointCaptured;
        public string monsterType;
        public string monsterSubType;
        public int teamId;
        public MatchPosition position;
        public int killerId;
        public long timestamp;
        public List<int> assistingParticipantIds;
        public string buildingType;
        public int victimId;
    }

    public class MatchPosition
    {
        public int x;
        public int y;
    }
}