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
    public IncrementAction(int amount = 1)
    {
        Amount = amount;
    }

    public int Amount { get; }
}

class IncrementReducer : Reducer<CountState, IncrementAction>
{
    public override CountState Reduce(CountState state, IncrementAction action)
    {
        return new CountState(state.Count + action.Amount);
    }
}
```

Bootstrapping

```csharp
var state = new CountState(count: 0);

var feature = new Feature<CountState>(state, reducers: new[]
{
    new IncrementReducer(),
});

var store = new Store(features: new[] 
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
