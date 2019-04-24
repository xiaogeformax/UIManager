using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// UI类型
    /// </summary>
    public enum UIType
    {
        Hud,
        Normal = 1,
        Popup = 2,
        Dialog = 4,
        Guide = 8,
        Tips = 16,
        TopMask = 32,
        Top = 64,

        Child,//子UI
    }

    /// <summary>
    /// UI加载方式
    /// </summary>
    public enum UIResType
    {
        Resorces,//使用Resource.Load()
        Bundle,//使用AssetBundle
        SetGameObject,//外部设置GameObject的方式（子UI）
    }

    /// <summary>
    /// UI动画状态
    /// </summary>
    public enum AnimationStateType
    {
        None,
        Start,
        Enable,
        Disable,
        Destroy,
    }

    /// <summary>
    /// UI状态
    /// </summary>
    public enum UIStateType
    {
        None,
        Awake,
        Start,
        Enable,
        Disable,
        Destroy,
    }

    public abstract class UI
    {
        public TaskCompletionSource<bool> Tcs = null;

        public UIData UiData = null;

        public GameObject GameObject = null;
        public Transform Transform = null;
        public Transform ChildParentNode = null;

        public UIStateType UIState = UIStateType.None;
        public AnimationStateType AnimationState = AnimationStateType.None;

        public bool AwakeState = false;
        public int SortingOrder = 0;
        public UIProxy UIProxy = null;

        protected Dictionary<Canvas, int> CanvasDic = null;
        protected Animator Animator = null;

        //当前是否播放动画
        protected TaskCompletionSource<bool> IsPlayingAniamtionTask = null;
        public void SetGameObject(GameObject gameObject)
        {
            this.GameObject = gameObject;
            this.GameObject.name = this.UiData.UiName;
            this.Transform = this.GameObject.transform;

            this.ChildParentNode = this.Transform.FindTransform("ChildParentNode");

            UIManager.Instance.SetUIParent(this, false);
        }
    }
}
