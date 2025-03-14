using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private static ViewManager instance;

    [SerializeField] private View _startingView;

    //all views
    [SerializeField] private View[] _views;

    private View _currentView;

    //history of opend views to go back
    private readonly Stack<View> _history = new Stack<View>();

    //get specific view
    public static T GetView<T>() where T : View
    {
        for (int i = 0; i < instance._views.Length; i++)
        {
            if (instance._views[i] is T tView)
            {
                return tView;
            }
        }

        return null;
    }

    public static void Show<T>(bool remember = true) where T : View
    {
        for (int i = 0; i < instance._views.Length; i++)
        {
            if (instance._views[i] is T tView)
            {
                RememberAndShow(remember, tView);
                break;
            }
        }
    }

    public static void Show(View view, bool remember = true)
    {
        RememberAndShow(remember, view);
    }

    //removes last view from Stack and shows it
    public static void ShowLast()
    {
        if (instance._history.Count != 0)
        {
            Show(instance._history.Pop(), false);
        }
    }

    public static bool IsGameOverlayViewActive() => GetView<GameOverlayView>().gameObject.activeSelf;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        //clear duplicates
        instance._views = instance._views.Distinct().ToArray<View>();

        for (int i = 0; i < instance._views.Length; i++)
        {
            _views[i].Initialize();

            _views[i].Hide();
        }

        if (_startingView != null)
        {
            Show(_startingView, true);
        }
    }

    private static void RememberAndShow<T>(bool remember, T view) where T : View
    {
        if (instance._currentView != null)
        {
            if (remember)
            {
                instance._history.Push(instance._currentView);
            }

            instance._currentView.Hide();
        }

        view.Show();

        instance._currentView = view;
    }

}
