using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

namespace UWP_project.Core.Graphic.Background.Strategy
{
	public interface IBackgroundStrategy
	{
		void OnAnimationSet(IField field);
		void DrawHook(CanvasDrawingSession draw);
	}
}
