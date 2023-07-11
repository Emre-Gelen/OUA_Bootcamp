using System;
using UnityEngine;

public class TimedTrigger
{
    public static void CreateTimer(float timeDuration, Action action)
    {
        new GameObject("TimedTrigger", typeof(MonoBehaviourHook)).GetComponent<MonoBehaviourHook>().OnUpdate = new TimedTrigger(timeDuration, action).Update;
    }

    private float _timeDuration;
    private float _remainingTime;
    private Action _actionToTrigger;

    public TimedTrigger(float timeDuration, Action action)
    {
        _timeDuration = timeDuration;
        _actionToTrigger = action;
        _remainingTime = timeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        _remainingTime -= Time.deltaTime;
        if (_remainingTime < 0)
        {
            _actionToTrigger();
            _remainingTime = _timeDuration;
        }
    }

    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action OnUpdate;
        private void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }
    }
}
