using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFramework
{
    /// <summary>
    /// 管理显示的堆栈
    /// </summary>
    public class UIStackContainer : IUIContainer
    {
        //避免push过快
        bool pushing = false;
        //避免pop过快
        bool poping = false;
        //正在关闭全部（避免在关闭过程中有些UI写了打开某个ui在disable、destroy里面）
        bool closingAll = false;

        public UIStackContainer(UIType uiType, int minOrder)
        {
            this.UIType = uiType;
            this.MinOrder = minOrder;
        }
        public UIType UIType;

        /// <summary>
        /// 该UI显示栈最小的order，起始order
        /// </summary>
        public int MinOrder = 0;

        //保存当前入栈的UI
        private CustomStack<UI> showStack = new CustomStack<UI>();

        /// <summary>
        /// 每个UI之间的order间隔
        /// </summary>
        private const int ORDER_PER_PANEL = 40;

        public UI Peek()
        {
            return showStack.Peek();
        }


        public void Open(string uiName, Action<UI> callback, params object[] args)
        {
            if (UIManager.Instance.ClosingAll)
                return;

            UI ui = UIManager.Instance.CreateUI(uiName);
            if (ui == null)
                return;

            OpenAsync(ui, callback, args);
        }
    }

}
