using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIFramework
{
    public class GameUI : UI
    {
        //保存子UI
        private UIChildContainer childUIContainer = new UIChildContainer();

        /// <summary>
        /// 等待所有动画播放完成
        /// </summary>
        public override async Task WaitAnimationFinished()
        {
            //等待自己动画播放完成
            await base.WaitAnimationFinished();
            //等待子UI动画播放完成
            await childUIContainer.WaitAnimationFinished();
        }
    }
}
