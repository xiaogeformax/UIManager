using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFramework;

namespace UIFramework
{
    /// <summary>
    /// 子UI管理器
    /// </summary>
    public class UIChildContainer: IUIContainer
    {
        //保存已加载的子UI
        private Dictionary<string, ChildUI> childDic = new Dictionary<string, ChildUI>();

        //保存已显示的子UI
        List<ChildUI> showList = new List<ChildUI>();


        public void Open(string uiName, Action<UI> callback, params object[] args)
        {
            ChildUI childUi = FindChildUi(uiName);

            if (childUi == null)
                return;

            OpenAsync(childUi, callback, args);
        }

        public void Open(UI ui, Action<UI> callback, params object[] args)
        {
            OpenAsync(ui as ChildUI, callback, args);
        }

        //通过名字查找子UI
        public ChildUI FindChildUi(string childUiName)
        {
            ChildUI childUi = null;
            childDic.TryGetValue(childUiName, out childUi);
            return childUi;
        }

        private async void OpenAsync(ChildUI childUi, Action<UI> callback, params object[] args)
        {
            if (childUi == null)
                return;

            await WaitAnimationFinished();
            UIManager.Instance.SetMask(true);
        }

        public async Task WaitAnimationFinished()
        {
            //等待子UI动画播放完成
            if (childDic != null)
            {
                foreach (var kv in childDic)
                    await kv.Value.WaitAnimationFinished();
            }
        }
    }

  
}
