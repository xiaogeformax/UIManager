﻿using System.Collections;
using System.Collections.Generic;
 

public abstract class EventListener : IBaseEventListener
{

    string[] Events = null;
    public abstract string[] OnGetEvents();

    public abstract void OnNotify(string evt, params object[] args);
    public bool HasEvents()
    {
        return Events != null;
    }

    public bool Contains(string evt)
    {
        if (Events == null || string.IsNullOrEmpty(evt))
            return false;

        for (int i = 0; i < Events.Length; i++)
        {
            string temp = Events[i];
            if (temp == evt)
                return true;
        }
        return false;
    }

    public void SetEvent(string[] events)
    {
        this.Events = events;
    }


}
