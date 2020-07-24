using System;
using System.Collections.Generic;
using System.Linq;
using Andification.Runtime.GridSystem;
using UnityEngine;

namespace Andification.Runtime
{
	public static class UnitPathManager
	{
		private static readonly float DiagonalGScore = Mathf.Sqrt(2);
		private static Dictionary<int, Vector2Int[]> UnitPaths { get; set; }

		public static Vector2Int[] GetUnitPath(int gateIndex)
		{
			return UnitPaths[gateIndex];
		}

		public static bool TryCalculatePaths(params Vector2Int[] extraBlockedCells)
		{
			UnitPaths = new Dictionary<int, Vector2Int[]>();
			Vector2Int exitPoint = GetExit();
			int index = 0;
			foreach (Vector2Int gatePoint in GetGates())
			{
				if (TryCalculatePath(gatePoint, exitPoint, extraBlockedCells, out List<Vector2Int> path))
				{
					UnitPaths.Add(index, path.ToArray());
				}
				else
				{
					return false;
				}

				index++;
			}

			return true;
		}

		private static Vector2Int GetExit()
		{
			throw new NotImplementedException();
		}

		private static IEnumerable<Vector2Int> GetGates()
		{
			throw new NotImplementedException();
		}

		private static bool TryCalculatePath(Vector2Int gate, Vector2Int exitPoint, IReadOnlyCollection<Vector2Int> extraBlockedCells, out List<Vector2Int> path)
		{
			//Start and exit
			Location startLocation = new Location(gate);
			Location exitLocation = new Location(exitPoint);
			
			//Keep track which location we are at
			Location curLocation = null;
			
			//Storage for locations
			List<Location> openList = new List<Location>();
			List<Location> closedList = new List<Location>();
			
			//Begin the storage with the start location
			openList.Add(startLocation);
			
			while (openList.Count > 0)
			{
				// get the square with the lowest F score
				float lowest = openList.Min(l => l.FScore);
				curLocation = openList.First(l => l.FScore == lowest);

				// add the current square to the closed list  
				closedList.Add(curLocation);

				// remove it from the open list  
				openList.Remove(curLocation);

				// if we added the destination to the closed list, we've found a path  
				if (closedList.FirstOrDefault(l => (l.X == exitLocation.X) && (l.Y == exitLocation.Y)) != null)
				{
					break;
				}

				foreach ((Location adjacentCell, bool diagonal) in GetWalkableAdjacentCells(curLocation.X, curLocation.Y, openList, extraBlockedCells))
				{
					float selfG = curLocation.GScore + (diagonal ? DiagonalGScore : 1);

					// if this adjacent square is already in the closed list, ignore it  
					if (closedList.FirstOrDefault(l => (l.X     == adjacentCell.X)
														&& (l.Y == adjacentCell.Y)) != null)
					{
						continue;
					}

					// if it's not in the open list...  
					if (openList.FirstOrDefault(l => (l.X   == adjacentCell.X)
													&& (l.Y == adjacentCell.Y)) == null)
					{
						// compute its score, set the parent  
						adjacentCell.GScore = selfG;
						adjacentCell.HScore = CalculateHScore(adjacentCell.X, adjacentCell.Y, exitLocation.X, exitLocation.Y);
						adjacentCell.FScore = adjacentCell.GScore + adjacentCell.HScore;
						adjacentCell.Parent = curLocation;

						// and add it to the open list  
						openList.Insert(0, adjacentCell);
					}
					else
					{
						// test if using the current G score makes the adjacent square's F score  
						// lower, if yes update the parent because it means it's a better path  
						if ((selfG + adjacentCell.HScore) < adjacentCell.FScore)
						{
							adjacentCell.GScore = selfG;
							adjacentCell.FScore = adjacentCell.GScore + adjacentCell.HScore;
							adjacentCell.Parent = curLocation;
						}
					}
				}
			}

			path = new List<Vector2Int>();
			if ((curLocation == null) || (curLocation.X != exitLocation.X) || (curLocation.Y != exitLocation.Y))
			{
				return false;
			}

			while (curLocation != null)
			{
				path.Add(new Vector2Int(curLocation.X, curLocation.Y));
				curLocation = curLocation.Parent;
			}

			return true;
		}

		private static IEnumerable<(Location, bool)> GetWalkableAdjacentCells(int startX, int startY, IReadOnlyCollection<Location> openList, IReadOnlyCollection<Vector2Int>
																				extraBlockedCells)
		{
			WorldGrid grid = null;
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					int xPos = startX + x;
					int yPos = startY + y;
					WorldGridCell targetCell = grid.GetCellAt(xPos, yPos);
					if ((targetCell == null)
						|| !targetCell.walkable
						|| ((extraBlockedCells.Count > 0) && extraBlockedCells.Contains(new Vector2Int(xPos, yPos))))
					{
						continue;
					}

					Location returnLocation = GetLocationFromPos(xPos, yPos) ?? new Location(xPos, yPos);

					yield return (returnLocation, (x != 0) && (y != 0));
				}
			}

			Location GetLocationFromPos(int x, int y)
			{
				foreach (Location location in openList)
				{
					if ((location.X == x) && (location.Y == y))
					{
						return location;
					}
				}

				return null;
			}
		}

		private static int CalculateHScore(int x, int y, int targetX, int targetY)
		{
			return Math.Abs(targetX - x) + Math.Abs(targetY - y);
		}

		private class Location
		{
			public readonly int X;
			public readonly int Y;
			public float FScore;
			public float GScore;
			public float HScore;
			public Location Parent;

			public Location(Vector2Int pos)
			{
				X = pos.x;
				Y = pos.y;
			}

			public Location(int xPos, int yPos)
			{
				X = xPos;
				Y = yPos;
			}
		}
	}
}