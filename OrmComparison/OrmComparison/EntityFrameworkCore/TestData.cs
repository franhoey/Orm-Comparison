using System;

namespace OrmComparison.EntityFrameworkCore
{
    public partial class TestData
    {
        public int IntData { get; set; }
        public Guid GuidData { get; set; }
        public string StringData { get; set; }
        public bool BoolData { get; set; }
        public DateTime DateData { get; set; }
    }
}
