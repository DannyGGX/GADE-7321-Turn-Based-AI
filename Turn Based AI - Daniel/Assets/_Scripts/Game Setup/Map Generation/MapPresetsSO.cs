using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace DannyG
{
	[CreateAssetMenu(fileName = "Map Presets", menuName = "Scriptable Object/Map Presets", order = 1)]
	public class MapPresetsSO : ScriptableObject
	{
		private readonly HashSet<int[,]> _mapList = new HashSet<int[,]>();

		public void Init()
		{
			_mapList.Add(_map0);
			_mapList.Add(_map1);
			_mapList.Add(_map2);
			_mapList.Add(_map5);
		}

		/// <summary>
		/// Choose a map equal to the index. Index is dependent on where a given map is in the hashset
		/// </summary>
		/// <param name="index"> Pass -1 to choose a random map </param>
		/// <returns></returns>
		public int[,] ChooseMap(int index)
		{
			if (index == -1)
			{
				index = Random.Range(0, _mapList.Count);
			}
			return _mapList.ElementAt(index);
		}
		
		private readonly int[,] _map0 =
		{
			{ 3, 3, 0, 0, 0, 0, 3, 0, 0, 3 },
			{ 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 3, 3, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 3, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
			{ 3, 3, 0, 0, 0, 0, 0, 0, 3, 3 },
			{ 3, 3, 3, 0, 0, 0, 0, 3, 3, 3 },
		};
		
		private readonly int[,] _map1 =
		{
			{ 3, 3, 3, 0, 0, 0, 0, 3, 3, 3 },
			{ 3, 3, 0, 0, 0, 0, 0, 0, 3, 3 },
			{ 3, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 3, 3, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 3, 3, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 3, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
			{ 3, 3, 0, 0, 0, 0, 0, 0, 3, 3 },
			{ 3, 3, 3, 0, 0, 0, 0, 3, 3, 3 },
		};
		private readonly int[,] _map2 =
		{
			{ 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
			{ 0, 0, 3, 0, 0, 0, 0, 0, 0, 3 },
			{ 0, 3, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 3, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 3, 0, 0, 0, 0, 3 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
			{ 0, 0, 0, 0, 0, 0, 0, 3, 3, 3 },
			{ 0, 3, 0, 0, 0, 0, 3, 3, 3, 3 },
		};
		private readonly int[,] _map3 = // blank
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		};
		private readonly int[,] _map4 = // 9 by 9 to test centering of board with odd number of tiles
		{
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		};
		private readonly int[,] _map5 =
		{
			{ 3, 3, 3, 3, 3, 0, 3, 3, 3, 3 },
			{ 3, 3, 3, 3, 0, 0, 0, 3, 3, 3 },
			{ 3, 3, 3, 0, 0, 0, 0, 0, 3, 3 },
			{ 3, 3, 0, 0, 0, 0, 0, 0, 0, 3 },
			{ 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
			{ 3, 0, 0, 0, 0, 0, 0, 0, 3, 3 },
			{ 3, 3, 0, 0, 0, 0, 0, 3, 3, 3 },
			{ 3, 3, 3, 0, 0, 0, 3, 3, 3, 3 },
			{ 3, 3, 3, 3, 0, 3, 3, 3, 3, 3 },
		};
	}
}
