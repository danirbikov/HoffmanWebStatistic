namespace HoffmanWebstatistic.Models.SerializerModels
{
    public class JSONSerializeModel
    {

        public class Rootobject
        {
            public Header header { get; set; }
            public Test[] tests { get; set; }
        }

        public class Header
        {
            public string standName { get; set; }
            public string VIN { get; set; }
            public string orderNum { get; set; }
            public string model { get; set; }
            public string @operator { get; set; }
            public string date { get; set; }
        }

        public class Test
        {
            public string nameTest { get; set; }
            public string testID { get; set; }
            public string testRes { get; set; }
            public string date { get; set; }
            public Value[] values { get; set; }
        }

        public class Value
        {
            public string varName { get; set; }
            public string varValue { get; set; }
        }

    }
}
