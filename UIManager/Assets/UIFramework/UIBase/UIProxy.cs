using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UGUIEventListener;
namespace UIFramework
{
    public abstract class UIProxy : EventListener
    {
        public void SetUi(UI ui)
        {
            this.UI = ui;
        }

        public UI UI;

        public override string[] OnGetEvents()
        {
            return null;
        }

        public abstract void OnAwake();

        public abstract void OnStart(params object[] args);

        public abstract void OnEnable();

        public abstract void OnDisable();

        public abstract void OnDestroy();

        public GameObject FindGameObject(string name)
        {
            if (UI == null || !UI.Transform)
                return null;
            return UI.Transform.FindGameObject(name);
        }

        public Transform FindTransform(string name)
        {
            if (UI == null || !UI.Transform)
                return null;
            return UI.Transform.FindTransform(name);
        }

        public void RegisterListener(string name, VoidDelegate handle, bool clear = true)
        {
            
        }
    }

}

