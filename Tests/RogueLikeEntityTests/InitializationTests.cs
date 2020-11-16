using System;
using SadRogue.Primitives;
using TheSadRogue.Integration;
using Xunit;

namespace Tests
{
    public class InitializationTests
    {
        private RogueLikeEntity entity;
        
        [Fact]
        public void NewFromColorsAndGlyphTest()
        {
            entity = new RogueLikeEntity(Color.Chartreuse, Color.Salmon, '1');
            
            Assert.Equal(Color.Chartreuse, entity.Foreground);
            Assert.Equal(Color.Salmon, entity.Background);
            Assert.Equal('1', entity.Glyph);
            Assert.Equal(new Point(0,0), entity.Position);
            Assert.True(entity.IsWalkable); //the default
            Assert.True(entity.IsTransparent); //the default
        }
        
        [Fact]
        public void NewFromPositionAndGlyphTest()
        {
            entity = new RogueLikeEntity((1,1), 2);
            Assert.Equal(Color.White, entity.Foreground);
            Assert.Equal(Color.Black, entity.Background);
            Assert.Equal(2, entity.Glyph);
            Assert.Equal(new Point(1,1), entity.Position);
        }
        
        [Fact]
        public void NewFromPositionColorAndGlyphTest()
        {
            entity = new RogueLikeEntity((1,3), Color.Cyan, 2);
            Assert.Equal(Color.Cyan, entity.Foreground);
            Assert.Equal(Color.Black, entity.Background);
            Assert.Equal(2, entity.Glyph);
            Assert.Equal(new Point(1,3), entity.Position);
        }
    }
}