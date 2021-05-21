﻿using UnityEngine.EventSystems;

public class MyEventSystem : EventSystem
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        EventSystem originalCurrent = current;
        current = this;
        base.Update();
        current = originalCurrent;
    }
}
