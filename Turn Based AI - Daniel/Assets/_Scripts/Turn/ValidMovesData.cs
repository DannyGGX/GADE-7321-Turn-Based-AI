
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace DannyG
{
	
	public struct ValidMovesData
	{
		public List<Coordinate> List;
		
		public ValidMovesData(List<Coordinate> list)
		{
			List = list;
		}
	}
}
