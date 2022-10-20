using System.Runtime.CompilerServices;
using MoonSharp.Interpreter;

namespace InfiniteChess.Shared; 

public static class Extensions {
	public static Position? ToPosition(this DynValue value) {
		if (value.Table is { } table) {
			if (table.Keys.Count() != 2) { return null; }
			
			if ((table[1] ?? table["x"] ?? table ["X"]) is not double xd || !IsInteger(xd)) { return null; }
			if ((table[2] ?? table["y"] ?? table["Y"]) is not double yd || !IsInteger(yd)) { return null; }
			return new((int)xd - 1, (int)yd - 1);
		}

		if (value.Tuple is { } tuple) {
			return tuple.Length == 2 && TryGetInt(tuple[0]) is { } x && TryGetInt(tuple[1]) is { } y
				? new Position(x - 1, y - 1) : null;
		}

		return null;

		bool IsInteger(double d)
			=> Math.Abs(d % 1) < 0.000001;

		int? TryGetInt(DynValue dyn)
			=> dyn.Type == DataType.Number && IsInteger(dyn.Number) ? (int)dyn.Number : null;
	}
}