﻿namespace TDFactory.Helper
{
    public class Relation
    {
        public string RelationName { get; set; }
        public string ForeignTable { get; set; }
        public string ForeignColumn { get; set; }
        public string PrimaryTable { get; set; }
        public string PrimaryColumn { get; set; }
    }
}
