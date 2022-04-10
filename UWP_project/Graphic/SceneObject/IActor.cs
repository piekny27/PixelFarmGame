using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UWP_project.Core;

namespace UWP_project.Graphic.SceneObject
{
    public interface IActor : IAnimatedObject, IPlacedInSpace, ISceneObject
    {
        SpaceDirection Direction { get; set; }

    }
}
