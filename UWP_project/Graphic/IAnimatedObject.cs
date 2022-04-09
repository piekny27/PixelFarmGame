using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;

namespace UWP_project.Graphic
{
	public interface IAnimatedObject : IPlacedInSpace, IDrawable
	{
		int Frame { get; }
		int AnimationSpeed { set; }
		string[] Animation { set; }
	}
}