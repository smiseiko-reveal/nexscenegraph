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
using System.Numerics;

namespace Veldrid.SceneGraph
{
    /// <summary>
    /// A Transform is a node which transforms all children
    /// </summary>
    public class Transform : Group, ITransform
    {
        public enum ReferenceFrameType
        {
            Relative,
            Absolute
        }
        
        public ReferenceFrameType ReferenceFrame { get; set; }

        protected Transform()
        {
            ReferenceFrame = ReferenceFrameType.Relative;
        }
        
        // Required for double-dispatch
        public override void Accept(INodeVisitor nv)
        {
            if (nv.ValidNodeMask(this))
            {
                nv.PushOntoNodePath(this);
                nv.Apply(this);
                nv.PopFromNodePath(this);
            };
        }

        public virtual bool ComputeLocalToWorldMatrix(ref Matrix4x4 matrix, NodeVisitor visitor)
        {
            if (ReferenceFrameType.Relative == ReferenceFrame)
            {
                return false;
            }

            matrix = Matrix4x4.Identity;
            return true;
        }
        
        public virtual bool ComputeWorldToLocalMatrix(ref Matrix4x4 matrix, NodeVisitor visitor)
        {
            if (ReferenceFrameType.Relative == ReferenceFrame)
            {
                return false;
            }

            matrix = Matrix4x4.Identity;
            return true;
        }

        public override IBoundingSphere ComputeBound()
        {
            var bsphere = base.ComputeBound();
            if (!bsphere.Valid()) return bsphere;

            var localToWorld = Matrix4x4.Identity;
            ComputeLocalToWorldMatrix(ref localToWorld, null);
           
            var xdash = bsphere.Center;
            xdash.X += bsphere.Radius;
            xdash = Vector3.Transform(xdash,localToWorld);

            var ydash = bsphere.Center;
            ydash.Y += bsphere.Radius;
            ydash = Vector3.Transform(ydash,localToWorld);

            var zdash = bsphere.Center;
            zdash.Z += bsphere.Radius;
            zdash = Vector3.Transform(zdash,localToWorld);

            bsphere.Center = Vector3.Transform(bsphere.Center, localToWorld);

            xdash -= bsphere.Center;
            var sqrlen_xdash = xdash.LengthSquared();

            ydash -= bsphere.Center;
            var sqrlen_ydash = ydash.LengthSquared();

            zdash -= bsphere.Center;
            var sqrlen_zdash = zdash.LengthSquared();

            bsphere.Radius = sqrlen_xdash;
            if (bsphere.Radius<sqrlen_ydash) bsphere.Radius = sqrlen_ydash;
            if (bsphere.Radius<sqrlen_zdash) bsphere.Radius = sqrlen_zdash;
            bsphere.Radius = (float)Math.Sqrt(bsphere.Radius);

            return bsphere;
            
        }
    }
}