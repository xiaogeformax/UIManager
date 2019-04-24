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

            //记录所有Canvas初始化的sortingOrder
            Canvas[] tempCanvases = this.GameObject.GetComponentsInChildren<Canvas>(true);
            CanvasDic = new Dictionary<Canvas, int>(tempCanvases.Length);
            for (int i = 0; i < tempCanvases.Length; i++)
            {
                Canvas tempCanvas = tempCanvases[i];
                CanvasDic[tempCanvas] = tempCanvas.sortingOrder;
            }
            if (this.UiData.HasAnimation)
                this.Animator = this.GameObject.GetComponent<Animator>();
            this.GameObject.SetActive(false);

            if (this.UiData.IsLuaUI)
            {
                //todo 创建lua代理
                //this.UIProxy = new UILuaProxy(this);
            }
            else
            {
                //创建Mono代理
                System.Type type = UIManager.Instance.GetType(this.UiData.UiName);
                this.UIProxy = System.Activator.CreateInstance(type) as UIProxy;
                this.UIProxy.SetUi(this);
            }
        }

        /// <summary>
        /// 设置界面层级
        /// </summary>
        /// <param name="order"></param>
        public void SetCavansOrder(int order)
        {
            this.SortingOrder = order;

            if (CanvasDic != null)
            {
                foreach (var kv in CanvasDic)
                    kv.Key.sortingOrder = kv.Value + order;
            }
        }

        #region Awake
        public virtual void Awake()
        {
            if (this.UIState == UIStateType.None)
            {
                this.UIState = UIStateType.Awake;
                AwakeState = true;

                this.OnAwake();
                UIManager.Instance.NotifyAwake(this);
                this.GameObject.SetActive(false);
            }
        }
        public void OnAwake()
        {
            this.UIProxy?.OnAwake();
        }

        #endregion
    }


}
