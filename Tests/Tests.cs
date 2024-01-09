using Requires;

namespace Tests;

[TestFixture]
public class RequireIsNullTests {
	[Test]
	public void IsNull_WithNullValue_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.IsNull((object) null));
	}

	[Test]
	public void IsNull_WithNonNullValue_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsNull("not null"));
	}

	[Test]
	public void IsNull_WithNonNullObject_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsNull(new object()));
	}
}

[TestFixture]
public class RequireIsNotNullTests {
	[Test]
	public void IsNotNull_WithNonNullValue_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.IsNotNull("not null"));
	}

	[Test]
	public void IsNotNull_WithNullValue_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsNotNull((object) null));
	}

	[Test]
	public void IsNotNull_WithNonNullValueType_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.IsNotNull(123));
	}
}

[TestFixture]
public class RequireIsTrueTests {
	[Test]
	public void IsTrue_WithTrueCondition_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.IsTrue(true));
	}

	[Test]
	public void IsTrue_WithFalseCondition_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsTrue(false));
	}

	[Test]
	public void IsTrue_WithFalseConditionAndMessage_ThrowsRequireExceptionWithMessage() {
		var exception = Assert.Throws<RequireException>(() => Require.IsTrue(false, "Custom message"));
		Assert.That(exception.Message, Does.Contain("Custom message"));
	}
}

[TestFixture]
public class RequireIsFalseTests {
	[Test]
	public void IsFalse_WithFalseCondition_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.IsFalse(false));
	}

	[Test]
	public void IsFalse_WithTrueCondition_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsFalse(true));
	}

	[Test]
	public void IsFalse_WithTrueConditionAndMessage_ThrowsRequireExceptionWithMessage() {
		var exception = Assert.Throws<RequireException>(() => Require.IsFalse(true, "Custom message"));
		Assert.That(exception.Message, Does.Contain("Custom message"));
	}
}

[TestFixture]
public class RequireAreEqualTests {
	[Test]
	public void AreEqual_WithEqualValues_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.AreEqual(5, 5));
	}

	[Test]
	public void AreEqual_WithUnequalValues_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.AreEqual(5, 6));
	}

	[Test]
	public void AreEqual_WithDifferentReferenceTypes_ThrowsRequireException() {
		var obj1 = new object();
		var obj2 = new object();
		Assert.Throws<RequireException>(() => Require.AreEqual(obj1, obj2));
	}

	[Test]
	public void AreEqual_WithNullAndNonNullObject_ThrowsRequireException() {
		object obj = null;
		Assert.Throws<RequireException>(() => Require.AreEqual(obj, new object()));
	}
}

[TestFixture]
public class RequireNotEqualTests {
	[Test]
	public void NotEqual_WithUnequalValues_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.NotEqual(5, 6));
	}

	[Test]
	public void NotEqual_WithEqualValues_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.NotEqual(5, 5));
	}

	[Test]
	public void NotEqual_WithSameReferenceInstance_ThrowsRequireException() {
		var obj = new object();
		Assert.Throws<RequireException>(() => Require.NotEqual(obj, obj));
	}

	[Test]
	public void NotEqual_WithNullAndNonNullObject_DoesNotThrow() {
		object obj = null;
		Assert.DoesNotThrow(() => Require.NotEqual(obj, new object()));
	}
}

[TestFixture]
public class RequireApproxEqualFloatTests {
	[TestCase(5.0f, 5.0f)]
	[TestCase(5.0f, 5.00001f)]
	[TestCase(-5.0f, -5.00001f)]
	public void ApproxEqual_Float_WithApproxEqualValues_DoesNotThrow(float expected, float actual) {
		Assert.DoesNotThrow(() => Require.ApproxEqual(expected, actual));
	}

	[TestCase(5.0f, 6.0f)]
	[TestCase(5.0f, -5.0f)]
	[TestCase(-5.0f, 5.0f)]
	public void ApproxEqual_Float_WithNotApproxEqualValues_ThrowsRequireException(float expected, float actual) {
		Assert.Throws<RequireException>(() => Require.ApproxEqual(expected, actual));
	}
}

[TestFixture]
public class RequireApproxEqualDoubleTests {
	[TestCase(5.0, 5.0)]
	[TestCase(5.0f, 5.00001f)]
	[TestCase(-5.0f, -5.00001f)]
	public void ApproxEqual_Double_WithApproxEqualValues_DoesNotThrow(double expected, double actual) {
		Assert.DoesNotThrow(() => Require.ApproxEqual(expected, actual));
	}

	[TestCase(5.0, 6.0)]
	[TestCase(5.0, -5.0)]
	[TestCase(-5.0, 5.0)]
	public void ApproxEqual_Double_WithNotApproxEqualValues_ThrowsRequireException(double expected, double actual) {
		Assert.Throws<RequireException>(() => Require.ApproxEqual(expected, actual));
	}
}

[TestFixture]
public class RequireInRangeTests {
	[Test]
	public void InRange_WithValueWithinRange_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.InRange(5, 2, 8));
	}

	[Test]
	public void InRange_WithValueBelowRange_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.InRange(1, 2, 8));
	}

	[Test]
	public void InRange_WithValueAboveRange_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.InRange(10, 2, 8));
	}

	[Test]
	public void InRange_WithValueEqualToMinimum_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.InRange(2, 2, 8));
	}

	[Test]
	public void InRange_WithValueEqualToMaximum_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.InRange(8, 2, 8));
	}

	[Test]
	public void InRange_WithMessage_ThrowsRequireExceptionWithMessage() {
		var exception = Assert.Throws<RequireException>(() => Require.InRange(1, 2, 8, "Custom message"));
		Assert.That(exception.Message, Does.Contain("Custom message"));
	}
}

[TestFixture]
public class RequireIsTypeTests {
	[Test]
	public void IsType_WithCorrectType_DoesNotThrow() {
		Assert.DoesNotThrow(() => Require.IsType<string>("hello"));
	}

	[Test]
	public void IsType_WithIncorrectType_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsType<int>("hello"));
	}

	[Test]
	public void IsType_WithNullValue_ThrowsRequireException() {
		Assert.Throws<RequireException>(() => Require.IsType<int>(null));
	}

	[Test]
	public void IsType_WithMessage_ThrowsRequireExceptionWithMessage() {
		var exception = Assert.Throws<RequireException>(() => Require.IsType<int>("hello", "Custom message"));
		Assert.That(exception.Message, Does.Contain("Custom message"));
	}
}
