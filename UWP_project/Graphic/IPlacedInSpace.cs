using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_project.Graphic
{
	public interface IPlacedInSpace
	{
		float X { get; set; }
		float Y { get; set; }
		double Width { get; }
		double Height { get; }
	}
}
