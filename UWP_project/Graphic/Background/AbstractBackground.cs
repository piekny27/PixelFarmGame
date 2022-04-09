using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using UWP_project.Core.Graphic.Background.Strategy;
using UWP_project.Graphic;

namespace UWP_project.Core.Graphic.Background
{

	public abstract class AbstractBackground : AbstractAnimatedObject, IBackground
	{
		LinkedList<IBackgroundStrategy> Strategies;

		public IField Field
		{
			get; private set;
		}

		public float Speed
		{
			get; set;
		}

		protected AbstractBackground(IField field)
		{
			Strategies = new LinkedList<IBackgroundStrategy>();
			Field = field;
			Speed = 1;
		}

		public void AddStrategy(IBackgroundStrategy strategy)
		{
			Strategies.AddLast(strategy);
		}

		protected override void OnAnimationSet()
		{
			base.OnAnimationSet();
			foreach (IBackgroundStrategy strategy in Strategies)
			{
				strategy.OnAnimationSet(Field);
			}
		}

		protected override void DrawHook(CanvasDrawingSession draw)
		{
			base.DrawHook(draw);
			foreach (IBackgroundStrategy strategy in Strategies)
			{
				strategy.DrawHook(draw);
			}
		}
	}
}
