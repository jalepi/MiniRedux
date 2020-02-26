# MiniRedux
A Minimalist Redux implementation for .NET written in C#

The classic Counter example

```csharp
class CountState
{
    public CountState(int count)
    {
        Count = count;
    }
    public int Count { get; }
}

class IncrementAction
{
    public IncrementAction(int amount = 1) => 
        this.Amount = amount;

    public int Amount { get; }
}

class IncrementReducer : Reducer<CountState, IncrementAction>
{
    public override CountState Reduce(CountState state, IncrementAction action) =>
        new CountState(state.Count + action.Amount);
}
```

Bootstrapping

```csharp
var state = new CountState(0);

var feature = new Feature<CountState>(state, reducers: new IReducer<CountState>[]
{
    new IncrementReducer(),
});

var store = new Store(features: new IReducible[]
{
    feature,
});
```

Basic usage

```csharp
Assert.Equal(expected: 0, actual: feature.State.Count);

await store.Dispatch(new IncrementAction(amount: 1));

Assert.Equal(expected: 1, actual: feature.State.Count);
```
