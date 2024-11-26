﻿using Microsoft.Xna.Framework;

namespace GrobbEventStreamHelper.Controls
{
    public class ProgressBar
    {
        public delegate double CurrentProgressFunction();

        public Rectangle Bounds;
        public string Text;
        public CurrentProgressFunction CurrentProgress;
    }
}
