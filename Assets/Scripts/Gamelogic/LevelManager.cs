using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    private int currentLevelIndex = 0;

    private void Awake()
    {
        //Disable all levels except the first one
        for (int i = 1; i < levels.Count; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
    }

    private void NextLevel()
    {
        levels[currentLevelIndex].gameObject.SetActive(false);
        currentLevelIndex++;
        if (currentLevelIndex < levels.Count)
        {
            levels[currentLevelIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    private void OnEnable()
    {
        foreach (Level level in levels)
        {
            level.OnLevelCompleted += NextLevel;
        }
    }

    private void OnDisable()
    {
        foreach (Level level in levels)
        {
            level.OnLevelCompleted -= NextLevel;
        }
    }
}
