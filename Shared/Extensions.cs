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

		if (value.Tuple is { Length: 2 } tuple
		    && TryGetInt(tuple[0], out var x)
		    && TryGetInt(tuple[1], out var y)) {
			return new(x - 1, y - 1);
		}

		return null;

		bool IsInteger(double d)
			=> Math.Abs(d % 1) < 0.000001;

		bool TryGetInt(DynValue dyn, out int i) {
			i = default;
			if (dyn.Type != DataType.Number || !IsInteger(dyn.Number)) { return false; }
			
			i = (int)dyn.Number;
			return true;
		}
	}

	public static IEnumerable<S> Choose<T, S>(this IEnumerable<T> @enum, Func<T, S?> mapper)
		=> @enum.Select(mapper).Where(x => x != null).Select(x => x!);

	public static IEnumerable<S> Choose<T, S>(this IEnumerable<T> @enum, Func<T, S?> mapper)
		where S : struct
		=> @enum.Select(mapper).Where(x => x != null).Select(x => x.Value);
}
