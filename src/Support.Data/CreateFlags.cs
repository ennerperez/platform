using System;

namespace Platform.Support.Data
{
    [Flags()]
    public enum CreateFlags
    {
        None = 0,
        ImplicitPK = 1,

        // create a primary key for field called 'Id' (Orm.ImplicitPkName)
        ImplicitIndex = 2,

        // create an index for fields ending in 'Id' (Orm.ImplicitIndexSuffix)
        AllImplicit = 3,

        // do both above
        AutoIncPK = 4

        // force PK field to be auto inc
    }
}