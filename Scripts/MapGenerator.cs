using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchV4;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public Match match;
    public MatchTimeline timeline;
    public RawImage mapImage;

    public void SetupUI(int participantId)
    {
        Vector2 size = new Vector2();
        switch(match.mapId)
        {
            case 11:
                size = new Vector2(15045, 15045);
                break;
            case 12:
                size = new Vector2(12877, 12877);
                break;
            case 21:
                size = new Vector2(12056, 12056);
                break;
        }
        mapImage.texture = ChampionsManager.instance.GetMapImage(match.mapId);
        foreach(MatchFrame frame in timeline.frames)
        {
            foreach(MatchEvent matchEvent in frame.events)
            {
                if (matchEvent.type == "CHAMPION_KILL")
                {
                    GameObject point = Instantiate(matchEvent.killerId < 6 ? Resources.Load<GameObject>("Prefabs/BluePoint") : Resources.Load<GameObject>("Prefabs/RedPoint"), mapImage.transform);
                    point.name = $"x:{matchEvent.position.x},y:{matchEvent.position.y}";
                    Rect rect = mapImage.GetComponent<RectTransform>().rect;
                    float x = rect.width * matchEvent.position.x / size.x;
                    float y = rect.height * matchEvent.position.y / size.y;
                    point.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                }
            }
        }
    }
}
