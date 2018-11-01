using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace EFCore.BulkExtensions.Tests
{
    public class TimestampTest : IDisposable
    {
        public void Dispose()
        {
        }

        public TimestampTest()
        {
            DeletePreviousDatabase();
        }

        [Fact]
        public void test_bulk_insert_with_timestamp()
        {
            using (var context = new TestContext(ContextUtil.GetOptions()))
            {
                var config = new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 4000,
                    SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints
                };

                var table1List = new List<Table1>
                {
                    new Table1{ Data = "ahmad"},
                    new Table1{ Data = "rahimi"}
                };

                context.BulkInsert(table1List, config);

                Assert.False(table1List[0].Id == 0);
            }
        }

        [Fact]
        public void test_bulk_insert_with_timestamp_when_IsConcurrencyToken_is_setted_for_property()
        {
            //this test will pass
            using (var context = new TestContext(ContextUtil.GetOptions()))
            {
                var config = new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 4000,
                    SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints
                };

                var table1List = new List<Table3>
                {
                    new Table3{ Data = "ahmad"},
                    new Table3{ Data = "rahimi"}
                };

                context.BulkInsert(table1List, config);

                Assert.False(table1List[0].Id == 0);
            }
        }

        [Fact]
        public void test_bulk_insert_with_timestamp_using_conversion()
        {
            using (var context = new TestContext(ContextUtil.GetOptions()))
            {
                var config = new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 4000,
                    SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints
                };

                var table1List = new List<Table2>
                {
                    new Table2{ Data = "ahmad"},
                    new Table2{ Data = "rahimi"}
                };

                context.BulkInsert(table1List, config);

                Assert.False(table1List[0].Id == 0);
            }
        }

        [Fact]
        public void test_bulk_insert_with_timestamp_when_ValueGeneratedOnAddOrUpdate_is_setted()
        {
            //this is normal behavior in ef so it is expected that we have this option here too
            using (var context = new TestContext(ContextUtil.GetOptions()))
            {
                var config = new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 4000,
                    SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints
                };

                var table1List = new List<Table3>
                {
                    new Table3{ Data = "ahmad"},
                    new Table3{ Data = "rahimi"}
                };

                context.BulkInsert(table1List, config);

                Assert.False(table1List[0].Id == 0);
                Assert.False(table1List[0].RowVersion == null);
            }
        }

        [Fact]
        public void test_bulk_insert_with_timestamp__using_conversion_and_ValueGeneratedOnAddOrUpdate_is_setted()
        {
            //this is normal behavior in ef so it is expected that we have this option here too
            using (var context = new TestContext(ContextUtil.GetOptions()))
            {
                var config = new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 4000,
                    SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints
                };

                var table1List = new List<Table2>
                {
                    new Table2{ Data = "ahmad"},
                    new Table2{ Data = "rahimi"}
                };

                context.BulkInsert(table1List, config);

                Assert.False(table1List[0].Id == 0);
                Assert.True(table1List[0].RowVersion > 0);
            }
        }

        private void DeletePreviousDatabase()
        {
            using (var context = new TestContext(ContextUtil.GetOptions()))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
