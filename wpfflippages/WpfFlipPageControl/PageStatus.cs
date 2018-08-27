using System;

namespace WpfFlipPageControl
{
	/// <summary>
	/// 页面状态
	/// </summary>
    public enum PageStatus
    {
        None,
		/// <summary>
		/// 拖拽中
		/// </summary>
        Dragging,
		/// <summary>
		/// 未捕获拖拽
		/// </summary>
        DraggingWithoutCapture,
		/// <summary>
		/// 放置动画
		/// </summary>
        DropAnimation,
		/// <summary>
		/// 翻页动画
		/// </summary>
        TurnAnimation
    }
}
