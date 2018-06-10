﻿using System.Collections;
using MonoGame.Extended.Sprites;
using Xunit;

namespace MonoGame.Extended.Entities.Tests
{
    public class DummyComponent
    {
    }

    public class AspectTests
    {
        private readonly ComponentManager _componentManager;
        private readonly BitArray _entityA;
        private readonly BitArray _entityB;

        public AspectTests()
        {
            _componentManager = new ComponentManager();
            _entityA = new BitArray(3)
            {
                [_componentManager.GetComponentTypeId(typeof(Transform2))] = true,
                [_componentManager.GetComponentTypeId(typeof(Sprite))] = true,
                [_componentManager.GetComponentTypeId(typeof(DummyComponent))] = true
            };
            _entityB = new BitArray(3)
            {
                [_componentManager.GetComponentTypeId(typeof(Transform2))] = true,
                [_componentManager.GetComponentTypeId(typeof(Sprite))] = true,
            };
        }

        [Fact]
        public void EmptyAspectMatchesAllComponents()
        {
            var componentManager = new ComponentManager();
            var emptyAspect = Aspect.All()
                .Build(componentManager);

            Assert.True(emptyAspect.IsInterested(_entityA));
            Assert.True(emptyAspect.IsInterested(_entityB));
        }

        [Fact]
        public void IsInterestedInAllComponents()
        {
            var allAspect = Aspect
                .All(typeof(Sprite), typeof(Transform2), typeof(DummyComponent))
                .Build(_componentManager);

            Assert.True(allAspect.IsInterested(_entityA));
            Assert.False(allAspect.IsInterested(_entityB));
        }

        [Fact]
        public void IsInterestedInEitherOneOfTheComponents()
        {
            var eitherOneAspect = Aspect
                .One(typeof(Transform2), typeof(DummyComponent))
                .Build(_componentManager);

            Assert.True(eitherOneAspect.IsInterested(_entityA));
            Assert.True(eitherOneAspect.IsInterested(_entityB));
        }

        [Fact]
        public void IsInterestedInJustOneComponent()
        {
            var oneAspect = Aspect
                .One(typeof(DummyComponent))
                .Build(_componentManager);

            Assert.True(oneAspect.IsInterested(_entityA));
            Assert.False(oneAspect.IsInterested(_entityB));
        }

        [Fact]
        public void IsInterestedInExcludingOneComponent()
        {
            var oneAspect = Aspect
                .Exclude(typeof(DummyComponent))
                .Build(_componentManager);

            Assert.False(oneAspect.IsInterested(_entityA));
            Assert.True(oneAspect.IsInterested(_entityB));
        }
    }
}