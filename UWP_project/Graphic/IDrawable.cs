using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;

namespace UWP_project.Graphic
{
	public interface IDrawable
	{
		void Draw(CanvasDrawingSession draw);
	}
}
