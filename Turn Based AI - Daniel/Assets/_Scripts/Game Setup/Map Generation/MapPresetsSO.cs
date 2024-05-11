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
		}
		public int[,] ChooseRandomMap()
		{
			return _mapList.ElementAt(Random.Range(0, _mapList.Count));
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
		private readonly int[,] _map3 =
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
	}
}
