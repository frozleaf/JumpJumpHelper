using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace JumpJumpHelper.Services.ImageProcessors.Contracts
{
    abstract class AbstractImageProcessor : IImageProcessor
    {
        protected Bitmap _bmp;

        public virtual int Width
        {
            get
            {
                if (_bmp != null)
                {
                    return _bmp.Width;
                }
                return -1;
            }
        }

        public virtual int Height
        {
            get
            {
                if (_bmp != null)
                {
                    return _bmp.Height;
                }
                return -1;
            }
        }

        public abstract System.Drawing.Color GetPixel(int x, int y);

        public abstract void Dispose();

        public Rectangle GetRectangle()
        {
            if(this.Width<=0 || this.Height<=0)
                return Rectangle.Empty;

            return new Rectangle(0,0,this.Width,this.Height);
        }
    }
}
