using Xunit;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestMain()
        {
            Program.Main(new string[0]);
        }

        [Fact]
        public void Normal_item_degration()
        {
            //arrange
            var expected = 19;
            var p = new Program() { Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Twice_item_degration_after_SellIn()
        {
            //arrange
            var expected1 = 19;
            var expected2 = 18;
            var p = new Program()
            {
                Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                                         new Item { Name = "+1 Dexterity Vest", SellIn = 0, Quality = 20 } }
            };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected1, p.Items[0].Quality);
            Assert.Equal(expected2, p.Items[1].Quality);
        }

        [Fact]
        public void Normal_sellin_decrement()
        {
            //arrange
            var expected = 9;
            var p = new Program() { Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].SellIn);
        }

        [Fact]
        public void Aged_brie_quality_increase()
        {
            //arrange
            var expected = 1;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Aged_brie_quality_increase_after_sellIn_is_zero()
        {
            //arrange
            var expected = 2;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 0 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Aged_brie_quality_50()
        {
            //arrange
            var expected = 50;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 50 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Legendary_sword_quality()
        {
            //arrange
            var expected = 80;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Legendary_sword_sellIn()
        {
            //arrange
            var expected = 0;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].SellIn);
        }

        [Fact]
        public void Backstage_passes_quality_increase_by_1()
        {
            //arrange
            var expected = 21;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } } };

            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Backstage_passes_quality_increase_by_2()
        {
            //arrange
            var expected = 22;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 8, Quality = 20 } } };

            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Backstage_passes_quality_increase_by_3()
        {
            //arrange
            var expected = 23;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 20 } } };

            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Backstage_passes_quality_drops_to_0()
        {
            //arrange
            var expected = 0;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } } };

            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Backstage_passes_quality_does_not_go_past_50()
        {
            //arrange
            var expected = 50;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 6, Quality = 49 } } };

            //act
            p.UpdateQuality();
            p.UpdateQuality();
            p.UpdateQuality();

            //assert
            Assert.True(p.Items[0].Quality <= expected);
        }

        [Fact]
        public void Item_Quality_Non_Negative()
        {
            //arrange

            var p = new Program()
            {
                Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                                                             new Item { Name = "+15 Dexterity Vest", SellIn = 10, Quality = 1 },
                                                             new Item { Name = "+10 Dexterity Vest", SellIn = 10, Quality = 0 } }
            };

            //act
            for (int i = 5; i > 0; i--)
            {
                p.UpdateQuality();
            }

            //assert
            for (int i = 0; i < p.Items.Count; i++)
            {
                Assert.True(p.Items[i].Quality >= 0);
            }
        }

        [Fact]
        public void Conjured_item_degration()
        {
            //arrange
            var expected = 4;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }

        [Fact]
        public void Conjured_item_degration_after_sellin()
        {
            //arrange
            var expected = 2;
            var p = new Program() { Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 6 } } };


            //act
            p.UpdateQuality();

            //assert
            Assert.Equal(expected, p.Items[0].Quality);
        }
    }
}