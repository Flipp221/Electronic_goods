using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_goods.Db
{
    public interface ISqlite
    {
        string GetDatabasePath(string filename);
    }
}
