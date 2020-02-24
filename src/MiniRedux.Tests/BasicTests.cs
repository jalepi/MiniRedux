using System.Threading.Tasks;
using Xunit;

namespace MiniRedux.Tests
{
    public class BasicTests
    {
        [Fact]
        public async Task CountStateIncrementByOneTest()
        {
            var state = new CountState(0);

            var feature = new Feature<CountState>(
                state: state,
                reducers: new IReducer<CountState>[]
                {
                    new IncrementReducer(),
                });

            var store = new Store(
                features: new IDispatcher[]
                {
                    feature,
                },
                effects: new IEffect[]
                {

                });

            Assert.Equal(expected: 0, actual: feature.State.Count);

            await store.Dispatch(new IncrementAction(amount: 1));

            Assert.Equal(expected: 1, actual: feature.State.Count);
        }


        [Fact]
        public async Task CountStateIncrementByOneWithSubscribersTest()
        {
            var state = new CountState(0);

            var feature = new Feature<CountState>(
                state: state,
                reducers: new IReducer<CountState>[]
                {
                    new IncrementReducer(),
                });

            var subscriber = new object();
            var observeCount = 0;

            var subscription = feature.Subscribe((prev, curr) =>
            {
                observeCount += 1;
                return Task.CompletedTask;
            });

            var store = new Store(
                features: new IDispatcher[]
                {
                    feature,
                },
                effects: new IEffect[]
                {

                });

            await store.Dispatch(new IncrementAction(amount: 1));
            Assert.Equal(expected: 1, actual: feature.State.Count);
            Assert.Equal(expected: 1, actual: observeCount);

            await store.Dispatch(new IncrementAction(amount: 1));
            Assert.Equal(expected: 2, actual: feature.State.Count);
            Assert.Equal(expected: 2, actual: observeCount);

            await store.Dispatch(new IncrementAction(amount: 1));
            Assert.Equal(expected: 3, actual: feature.State.Count);
            Assert.Equal(expected: 3, actual: observeCount);

            subscription.Dispose();

            await store.Dispatch(new IncrementAction(amount: 1));
            Assert.Equal(expected: 4, actual: feature.State.Count);
            Assert.Equal(expected: 3, actual: observeCount);

            await store.Dispatch(new IncrementAction(amount: 1));
            Assert.Equal(expected: 5, actual: feature.State.Count);
            Assert.Equal(expected: 3, actual: observeCount);
        }
    }

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
}