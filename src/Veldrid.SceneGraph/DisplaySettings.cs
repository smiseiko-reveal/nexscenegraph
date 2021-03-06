﻿//
// Copyright 2018 Sean Spicer 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Runtime.InteropServices;

namespace Veldrid.SceneGraph
{
    /// <summary>
    /// Singleton class to capture information required for rendering
    /// </summary>
    public class DisplaySettings : IDisplaySettings
    {
        private static readonly Lazy<IDisplaySettings> lazy = new Lazy<IDisplaySettings>(() => new DisplaySettings());

        public static IDisplaySettings Instance => lazy.Value;

        public float ScreenWidth { get; set; }
        public float ScreenHeight { get; set; }
        public float ScreenDistance { get; set; }
        
        public GraphicsBackend GraphicsBackend { get; private set; }
        
        private DisplaySettings()
        {
            SetDefaults();
            ReadEnvironmentVariables();
        }

        private void ReadEnvironmentVariables()
        {
        }

        private void SetDefaults()
        {
            bool isMacOS = RuntimeInformation.OSDescription.Contains("Darwin");
            if (isMacOS)
            {
                GraphicsBackend = GraphicsBackend.Metal;
            }
            else
            {
                GraphicsBackend = GraphicsBackend.Direct3D11;
            }
        }
    }
}