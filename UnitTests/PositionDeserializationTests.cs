using MoonSharp.Interpreter;

namespace InfiniteChess.UnitTests; 

public class PositionDeserializationTests {
	private readonly Script _script = new(CoreModules.Preset_HardSandbox);

	public PositionDeserializationTests() {
		UserData.RegisterType<Position>();
	}

	[Fact]
	public void TestTable() {
		Assert.Equal(new Position(0, 1), _script.DoString("return { 1, 2 }").ToPosition());
	}

	[Fact]
	public void TestNamedTable() {
		Assert.Equal(new Position(0, 1), _script.DoString("return { x = 1, y = 2 }").ToPosition());
	}

	[Fact]
	public void TestTableCase() {
		Assert.Equal(new Position(0, 1), _script.DoString("return { X = 1, y = 2 }").ToPosition());
		Assert.Equal(new Position(0, 1), _script.DoString("return { x = 1, Y = 2 }").ToPosition());
		Assert.Equal(new Position(0, 1), _script.DoString("return { X = 1, Y = 2 }").ToPosition());
	}

	[Fact]
	public void TestTuple() {
		Assert.Equal(new Position(0, 1), _script.DoString("return 1, 2").ToPosition());
	}

	[Fact]
	public void TestFailLongTable() {
		Assert.Null(_script.DoString("return { x = 1, y = 2, z = 3 }").ToPosition());
	}

	[Fact]
	public void TestFailLongTuple() {
		Assert.Null(_script.DoString("return 1, 2, 3").ToPosition());
	}

	[Fact]
	public void TestFailRoundTable() {
		Assert.Null(_script.DoString("return { X = 1.4, Y = 2 }").ToPosition());
	}

	[Fact]
	public void TestFailRoundTuple() {
		Assert.Null(_script.DoString("return 1, 2.8").ToPosition());
	}

	[Fact]
	public void TestFailWrongType() {
		Assert.Null(_script.DoString("return nil").ToPosition());
	}
}
