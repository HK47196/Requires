using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Requires;

[Serializable]
public class RequireException : Exception {
	public RequireException() {
	}

	public RequireException(string message) : base(message) {
	}

	public RequireException(string message, Exception inner) : base(message, inner) {
	}
}

file static class ThrowHelper {
	[DoesNotReturn]
	[MethodImpl(MethodImplOptions.NoInlining)]
	[StackTraceHidden]
	[DebuggerHidden]
	public static void Throw(string message) {
		throw new RequireException(message);
	}
}

public static class Require {
	[ContractAnnotation("value:notnull => halt")]
	[AssertionMethod]
	public static void IsNull<T>(T? value,
		string message = "",
		[CallerArgumentExpression("value")] string value_expression = "") {
		if (value == null) {
			return;
		}

		var full_message = $"Requirement failed. Expected ({value_expression}) to be null. {message}";
		ThrowHelper.Throw(full_message);
	}

	[ContractAnnotation("value:null => halt")]
	[AssertionMethod]
	public static void IsNotNull<T>([System.Diagnostics.CodeAnalysis.NotNull] T? value,
		string message = "",
		[CallerArgumentExpression("value")] string value_expression = "") {
		if (value != null) {
			return;
		}

		var full_message = $"Requirement failed. Expected ({value_expression}) to be non-null. {message}";
		ThrowHelper.Throw(full_message);
	}

	[ContractAnnotation("condition:false => halt")]
	[AssertionMethod]
	public static void IsTrue([DoesNotReturnIf(false)] bool condition,
		string message = "",
		[CallerArgumentExpression("condition")] string expected_expression = "") {
		if (condition) {
			return;
		}

		var full_message = $"Requirement failed. Expected ({expected_expression}) to evaluate to False. {message}";
		ThrowHelper.Throw(full_message);
	}

	[ContractAnnotation("condition:true => halt")]
	[AssertionMethod]
	public static void IsFalse([DoesNotReturnIf(true)] bool condition,
		string message = "",
		[CallerArgumentExpression("condition")] string expected_expression = "") {
		if (!condition) {
			return;
		}

		var full_message = $"Requirement failed. Expected ({expected_expression}) to evaluate to False. {message}";
		ThrowHelper.Throw(full_message);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void AreEqual<T, U>(T? expected,
		U? actual,
		string message = "",
		[CallerArgumentExpression("expected")] string expected_expression = "",
		[CallerArgumentExpression("actual")] string actual_expression = "") {
		//TODO: create overload for struct to avoid boxing?
		if (Equals(expected, actual)) {
			return;
		}

		var full_message = $"Requirement failed. Expected {expected_expression} == {actual_expression}. {message}";
		ThrowHelper.Throw(full_message);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotEqual<T, U>(T? expected,
		U? actual,
		string message = "",
		[CallerArgumentExpression("expected")] string expected_expression = "",
		[CallerArgumentExpression("actual")] string actual_expression = "") {
		//TODO: create overload for struct to avoid boxing?
		if (!Equals(expected, actual)) {
			return;
		}

		var full_message = $"Requirement failed. Expected {expected_expression} != {actual_expression}. {message}";
		ThrowHelper.Throw(full_message);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ApproxEqual(float expected,
		float actual,
		string message = "",
		[CallerArgumentExpression("expected")] string expected_expression = "",
		[CallerArgumentExpression("actual")] string actual_expression = "") {
		//TODO: create overload for struct to avoid boxing?
		if (Util.IsApproxEqual(expected, actual)) {
			return;
		}

		var full_message = $"Requirement failed. Expected {expected_expression} ~= {actual_expression}. {message}";
		ThrowHelper.Throw(full_message);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ApproxEqual(double expected,
		double actual,
		string message = "",
		[CallerArgumentExpression("expected")] string expected_expression = "",
		[CallerArgumentExpression("actual")] string actual_expression = "") {
		//TODO: create overload for struct to avoid boxing?
		if (Util.IsApproxEqual(expected, actual)) {
			return;
		}

		var full_message = $"Requirement failed. Expected {expected_expression} ~= {actual_expression}. {message}";
		ThrowHelper.Throw(full_message);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void InRange<T>(T value, T minimum, T maximum, string message = "") where T : IComparable<T> {
		if (value.CompareTo(minimum) >= 0 && value.CompareTo(maximum) <= 0) {
			return;
		}

		var full_message = $"Requirement failed. Expected {value} to be in range [{minimum}, {maximum}]. {message}";
		ThrowHelper.Throw(full_message);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IsType<T>(object? obj, string message = "") {
		if (obj is T) {
			return;
		}

		var full_message =
			$"Requirement failed. Expected object of type {typeof(T)}, but found {obj?.GetType()}. {message}";
		ThrowHelper.Throw(full_message);
	}
}
