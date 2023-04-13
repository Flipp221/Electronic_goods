using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Electronic_goods.Db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Electronic_goods.Droid
{
    class SQlite_Android : ISqlite
    {
        public string GetDatabasePath(string filename)
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, filename);
            return path;
        }
    }
}