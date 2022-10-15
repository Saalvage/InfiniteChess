namespace InfiniteChess.Client;

public static class Extensions {
	public static void Fill<T>(this Array array, T value) {
		var elementType = array.GetType().GetElementType();
		if (elementType is null) {
			throw new ArgumentException("Array without element type", nameof(array));
		}

		if (elementType.IsValueType && value == null) {
			throw new ArgumentNullException(nameof(value));
		}

		if (elementType.IsAssignableFrom(value?.GetType()) is not true && value != null) {
			throw new ArgumentException("Must be assignable to array's element type", nameof(value));
		}

		if (array.Length == 0) { return; }
		
		var indices = new int[array.Rank];
		while (true) {
			array.SetValue(value, indices);
			for (var i = 0; i < array.Rank; i++) {
				indices[i]++;
				if (indices[i] < array.GetLength(i)) { break; }
				if (i == array.Rank - 1) { return; }
				indices[i] = 0;
			}
		}
	}
}
