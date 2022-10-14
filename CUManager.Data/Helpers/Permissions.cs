﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CUManager.Data.Helpers
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static class AllPermissions
        {
            public const string PostView = "Permissions.Posts.View";
            public const string PostEdit = "Permissions.Posts.Edit";
            public const string PostCreate = "Permissions.Posts.Create";
            public const string PostDelete = "Permissions.Posts.Delete";

            public static List<string> GetPermissionList()
            {
                Type t = typeof(AllPermissions);
                FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);

                var tt = new List<string>();
                foreach (FieldInfo fi in fields)
                {
                    tt.Add(fi.GetValue(null).ToString());
                }
                return tt;
            }
        }
    }
}
