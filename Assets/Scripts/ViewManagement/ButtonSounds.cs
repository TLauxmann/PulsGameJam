using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private void Awake()
    {
        RegisterButtonSounds();
    }

    private void RegisterButtonSounds()
    {
        EventTrigger.Entry eventtype = new EventTrigger.Entry();
        eventtype.eventID = EventTriggerType.PointerEnter;
        eventtype.callback.AddListener((eventData) => AudioManager.instance.FindSoundByName("HoverButton").source?.Play());

        foreach (Button b in FindObjectsOfType<Button>(true))
        {
            b.onClick.AddListener(() => AudioManager.instance.FindSoundByName("ClickButton").source?.Play());
            b.gameObject.AddComponent<EventTrigger>();
            b.gameObject.GetComponent<EventTrigger>().triggers.Add(eventtype);
        }
    }
}
