using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerInputReader playerInput;
    [SerializeField] private List<Level> levels;
    [SerializeField] private AudioSource audioSource;
    private int currentLevelIndex = 0;

    private void Awake()
    {
        //Disable all levels except the first one
        for (int i = 1; i < levels.Count; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        LevelStarts();
    }

    private void LevelStarts()
    {
        audioSource.Play();
    }

    private void LevelCompleted()
    {
        StartCoroutine(LevelEndSequenze());
    }

    private IEnumerator LevelEndSequenze()
    {
        playerInput.DisablePlayerActions();
        yield return new WaitForSeconds(3f);
        LoadNextLevel();
        playerInput.EnablePlayerActions();
    }
    private void LoadNextLevel()
    {
        levels[currentLevelIndex].gameObject.SetActive(false);
        currentLevelIndex++;
        if (currentLevelIndex < levels.Count)
        {
            levels[currentLevelIndex].gameObject.SetActive(true);
            LevelStarts();
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
            level.OnLevelCompleted += LevelCompleted;
        }
    }

    private void OnDisable()
    {
        foreach (Level level in levels)
        {
            level.OnLevelCompleted -= LevelCompleted;
        }
    }
}
