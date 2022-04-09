using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UWP_project.Core.Graphic.Background.Strategy;
using UWP_project.Graphic;

namespace UWP_project.Core.Graphic.Background
{
	public interface IBackground : IAnimatedObject
	{
		void AddStrategy(IBackgroundStrategy strategy);
		float Speed { get; set; }
	}
}
