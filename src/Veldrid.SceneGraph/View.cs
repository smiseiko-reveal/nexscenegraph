﻿//
// Copyright (c) 2018 Sean Spicer
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//

using System;

namespace Veldrid.SceneGraph
{
    public class View
    {
        public ICamera Camera { get; set; }

        public View()
        {
            Camera = Veldrid.SceneGraph.Camera.Create(DisplaySettings.Instance.ScreenWidth, DisplaySettings.Instance.ScreenHeight);
            Camera.View = this;

            var height = DisplaySettings.Instance.ScreenHeight;
            var width = DisplaySettings.Instance.ScreenWidth;
            var dist = DisplaySettings.Instance.ScreenDistance;
            
            // TODO: This is tricky - need to fix when ViewAll implemented
            var vfov = (float) Math.Atan2(height / 2.0f, dist) * 2.0f; 

            Camera.SetProjectionMatrixAsPerspective(vfov, width / height, 0.1f, 100f);
        }
    }
}