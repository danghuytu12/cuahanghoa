﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuahanghoa.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins { get { if (_ins == null) _ins = new DataProvider(); return _ins; } set { _ins = value; } }
        public CuahanghoaEntities1 DB { get; set; }
        private DataProvider()
        {
            DB = new CuahanghoaEntities1();
        }

    }
}
