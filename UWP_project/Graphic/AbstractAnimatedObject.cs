using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using System.Numerics;


namespace UWP_project.Graphic
{
	public abstract class AbstractAnimatedObject : IAnimatedObject
	{
		private const int DEFAULT_ANIMATION_SPEED = 15; //frame rate 60fps
		private int FrameIndex = 0;
		private int animationSpeed;
		private string[] textures = null;
		private CanvasBitmap[] bitmaps = null;

		protected AbstractAnimatedObject()
		{
			AnimationSpeed = DEFAULT_ANIMATION_SPEED;
		}

		public float X
		{
			get; set;
		} = 0;
		public float Y
		{
			get; set;
		} = 0;
		public double Width
		{
			get; protected set;
		} = 0;
		public double Height
		{
			get; protected set;
		} = 0;

		public int Frame
		{
			get; protected set;
		} = 0;
		public int AnimationSpeed
		{
			protected get
			{
				return animationSpeed;
			}
			set
			{
				animationSpeed = value > 0 ? value : 1;
			}
		}
		public string[] Animation
		{
			set
			{
				textures = value;

				//Null parameter disables animation
				if (value == null)
				{
					this.bitmaps = null;
					return;
				}

				bitmaps = TextureLoader.Instance[value];
				Frame = 0;

				var size = bitmaps[Frame].Size;
				Width = size.Width;
				Height = size.Height;

				OnAnimationSet();
			}
			protected get
			{
				return textures;
			}
		}


		public void Draw(CanvasDrawingSession draw)
		{
			FrameIndex++;
			if (FrameIndex >= AnimationSpeed)
			{
				FrameIndex = 0;
				Frame++;
				if (Frame >= textures.GetLength(0) || Frame >= bitmaps.GetLength(0))
				{
					Frame = 0;
				}
			}

			ICanvasImage bitmap = bitmaps[Frame];

			if (bitmap == null)
			{
				return;
			}

			draw.DrawImage(bitmap, new Vector2(X, Y));

			var size = bitmaps[Frame].Size;
			Width = size.Width;
			Height = size.Height;

			DrawHook(draw);
		}
		protected virtual void DrawHook(CanvasDrawingSession draw)
		{
		}
		protected virtual void OnAnimationSet()
		{
		}
	}
}
