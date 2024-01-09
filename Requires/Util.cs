namespace Requires;

public static class Util {
	private const double Epsilon = 0.00001;

	//Lifted from Godot.

	public static bool IsApproxEqual(float a, float b) {
		// Check for exact equality first, required to handle "infinity" values.
		// ReSharper disable once CompareOfFloatsByEqualityOperator
		if (a == b) {
			return true;
		}

		// Then check for approximate equality.
		var tolerance = (float) Epsilon * Math.Abs(a);
		if (tolerance < (float) Epsilon) {
			tolerance = (float) Epsilon;
		}

		return Math.Abs(a - b) < tolerance;
	}

	public static bool IsApproxEqual(double a, double b) {
		// Check for exact equality first, required to handle "infinity" values.
		// ReSharper disable once CompareOfFloatsByEqualityOperator
		if (a == b) {
			return true;
		}

		// Then check for approximate equality.
		var tolerance = Epsilon * Math.Abs(a);
		if (tolerance < Epsilon) {
			tolerance = Epsilon;
		}

		return Math.Abs(a - b) < tolerance;
	}
}
