using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using JumpJumpHelper.Services.ImageProcessors.Contracts;

namespace JumpJumpHelper.Services.ImageProcessors
{
    class DefaultImageProcessor : AbstractImageProcessor
    {
        public DefaultImageProcessor(Bitmap bitmap)
        {
            this._bmp = bitmap;
        }

        public override System.Drawing.Color GetPixel(int x, int y)
        {
            return this._bmp.GetPixel(x, y);
        }

        public override void Dispose()
        {
            
        }
    }
}
