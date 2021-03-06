﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Simple.Data.OData.Tests
{
    public class FindExtraClausesTest : TestBase
    {
        [Fact]
        public void AllSelect()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .Select(_db.Products.ProductID);

            Assert.True(products.First().ProductID > 0);
            Assert.Throws<RuntimeBinderException>(() => products.First().ProductName);
        }

        [Fact]
        public void AllTake()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .Take(1);

            Assert.Equal(1, products.Count());
        }

        [Fact]
        public void AllSkip()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .Skip(1);

            Assert.Equal(76, products.Count());
        }

        [Fact]
        public void AllSkipTake()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .Skip(2)
                .Take(1);

            Assert.Equal(1, products.Count());
        }

        [Fact]
        public void AllOrderByAscending()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .OrderBy(_db.Products.ProductName);

            Assert.Equal("Alice Mutton", products.First().ProductName);
        }

        [Fact]
        public void AllOrderByDescending()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .OrderByDescending(_db.Products.ProductName);

            Assert.Equal("Zaanse koeken", products.First().ProductName);
        }

        [Fact]
        public void AllOrderByDescendingSelect()
        {
            IEnumerable<dynamic> products = _db.Products.All()
                .OrderByDescending(_db.Products.ProductName)
                .Select(_db.Products.ProductName);

            Assert.Equal("Zaanse koeken", products.First().ProductName);
        }
    }
}
