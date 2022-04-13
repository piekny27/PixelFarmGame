using UWP_project.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_project.Core
{

	public class SpaceDirection
	{
		public enum HorizontalDirection
		{
			LEFT, NONE, RIGHT
		}

		public enum VerticalDirection
		{
			UP, NONE, DOWN
		}

		static readonly SpaceDirection LEFT_UP = new SpaceDirection(HorizontalDirection.LEFT, VerticalDirection.UP);
		static readonly SpaceDirection NONE_UP = new SpaceDirection(HorizontalDirection.NONE, VerticalDirection.UP);
		static readonly SpaceDirection RIGHT_UP = new SpaceDirection(HorizontalDirection.RIGHT, VerticalDirection.UP);
		static readonly SpaceDirection LEFT_DOWN = new SpaceDirection(HorizontalDirection.LEFT, VerticalDirection.DOWN);
		static readonly SpaceDirection NONE_DOWN = new SpaceDirection(HorizontalDirection.NONE, VerticalDirection.DOWN);
		static readonly SpaceDirection RIGHT_DOWN = new SpaceDirection(HorizontalDirection.RIGHT, VerticalDirection.DOWN);
		static readonly SpaceDirection LEFT_NONE = new SpaceDirection(HorizontalDirection.LEFT, VerticalDirection.NONE);
		static readonly SpaceDirection NONE_NONE = new SpaceDirection(HorizontalDirection.NONE, VerticalDirection.NONE);
		static readonly SpaceDirection RIGHT_NONE = new SpaceDirection(HorizontalDirection.RIGHT, VerticalDirection.NONE);

		public HorizontalDirection Horizontal
		{
			get; protected set;
		}
		public VerticalDirection Vertical
		{
			get; protected set;
		}

		public static SpaceDirection None
		{
			get
			{
				return NONE_NONE;
			}
		}

		private SpaceDirection(HorizontalDirection horizontal, VerticalDirection vertical)
		{
			Horizontal = horizontal;
			Vertical = vertical;
		}

		public static SpaceDirection Get(HorizontalDirection horizontal, VerticalDirection vertical)
		{
			if(horizontal == HorizontalDirection.LEFT && vertical == VerticalDirection.UP)			return LEFT_UP;
			else if(horizontal == HorizontalDirection.LEFT && vertical == VerticalDirection.DOWN)	return LEFT_DOWN;
			else if (horizontal == HorizontalDirection.LEFT && vertical == VerticalDirection.NONE)  return LEFT_NONE;
			else if (horizontal == HorizontalDirection.RIGHT && vertical == VerticalDirection.UP)	return RIGHT_UP;
			else if (horizontal == HorizontalDirection.RIGHT && vertical == VerticalDirection.DOWN) return RIGHT_DOWN;
			else if (horizontal == HorizontalDirection.RIGHT && vertical == VerticalDirection.NONE) return RIGHT_NONE;
			else if (horizontal == HorizontalDirection.NONE && vertical == VerticalDirection.UP)	return NONE_UP;
			else if (horizontal == HorizontalDirection.NONE && vertical == VerticalDirection.DOWN)	return NONE_DOWN;
			else return NONE_NONE;
		}
	}
}
