using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using JumpJumpHelper.Services.ImageProcessors.Contracts;

namespace JumpJumpHelper.Services.ImageProcessors
{
    class MemoryImageProcessor : AbstractImageProcessor
    {
        private BitmapData _bitmapData = null;
        private byte[] _bitmapBytes = null;
        private int _occupiedBytesEachColor=-1;

        public MemoryImageProcessor(Bitmap bitmap)
        {
            this._bmp = bitmap;
            // 获取PixelFormat
            PixelFormat pixelFormat = this._bmp.PixelFormat;
            // 获取BitmapData
            this._bitmapData = this._bmp.LockBits(this.GetRectangle(), ImageLockMode.ReadWrite, pixelFormat);
            // 获取第一个字节的索引位置
            IntPtr ptr = this._bitmapData.Scan0;
            // 计算每个颜色所占的字节数
            switch (pixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    _occupiedBytesEachColor = 4;
                    break;
                case PixelFormat.Format24bppRgb:
                    _occupiedBytesEachColor = 3;
                    break;
                default:
                    throw new Exception("不支持该PixelFormat类型：" + this._bmp.PixelFormat);
            }
            // 拷贝颜色索引字节到内存中
            _bitmapBytes = new byte[_bitmapData.Stride * this.Height];
            Marshal.Copy(ptr, _bitmapBytes, 0, _bitmapBytes.Length);
        }

        public override System.Drawing.Color GetPixel(int x, int y)
        {
            // 每行字节数
            int rowByteCount = this._bitmapData.Stride;
            // 计算颜色索引：每行字节数*行号+列号*每个颜色所占字节数
            int index = rowByteCount * y + x * _occupiedBytesEachColor;
            // 颜色依次为BGRA
            byte b = _bitmapBytes[index];
            byte g = _bitmapBytes[index + 1];
            byte r = _bitmapBytes[index + 2];
            byte a = 255; // 默认使用255，不透明
            if (_occupiedBytesEachColor > 3)
            {
                a = _bitmapBytes[index + 3];
            }

            return Color.FromArgb(a,r,g,b);
        }

        public override void Dispose()
        {
            if (this._bmp!=null && _bitmapData != null)
            {
                this._bmp.UnlockBits(_bitmapData);
            }
        }
    }
}
