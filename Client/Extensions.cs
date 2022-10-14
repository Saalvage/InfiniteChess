namespace InfiniteChess.Client;

public static class Extensions {
	public static void Fill<T>(this Array arr, T obj) {
		if (arr.Length == 0) { return; }

		var indices = new int[arr.Rank];
		while (true) {
			arr.SetValue(obj, indices);
			for (var j = 0; j < arr.Rank; j++) {
				indices[j]++;
				if (indices[j] < arr.GetLength(j)) { break; }
				if (j == arr.Rank - 1) { return; }
				indices[j] = 0;
			}
		}
	}
}
