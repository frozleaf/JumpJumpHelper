using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JumpJumpHelper.Services.ImageProcessors.Contracts
{
    /// <summary>
    /// 图像处理器
    /// </summary>
    interface IImageProcessor : IDisposable
    {
        /// <summary>
        /// 宽度
        /// </summary>
        int Width { get; }
        /// <summary>
        /// 高度
        /// </summary>
        int Height { get; }
        /// <summary>
        /// 获取指定点像素颜色
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns></returns>
        Color GetPixel(int x, int y);
    }
}
