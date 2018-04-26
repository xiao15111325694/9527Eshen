using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Repository_基础结构层.Models;

namespace MVCEchartsManager.DataServer
{
    public class HelpDAl
    {
        public List<string> GetDisplayName<T>(T eneityView)
        {
            PropertyInfo[] peroperties = eneityView.GetType().GetProperties();
            List<string> list= new List<string>();
            foreach (var peropertie in peroperties)
            {
                var objs = peropertie.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (objs != null)
                {
                    var disPlayName = ((DisplayNameAttribute)objs[0]).DisplayName;
                    list.Add(disPlayName);
                }
            }
            return list;
        }


        public List<string> GetValue<T>(T eneityView)
        {
                List<string> result = new List<string>();
                Type type = eneityView.GetType();
                PropertyInfo[] Propertys = type.GetProperties();

                foreach (var Property in Propertys)
                {
                    string name = Property.Name;;
                    PropertyInfo properinfo = type.GetProperty(name);
                    var valueEntity=  properinfo.GetValue(eneityView).ToString();
                    result.Add(valueEntity);
                }


            return result;
        }

        public static IQueryable<T> OrderBy<T>(IQueryable<T> query, string ordering, params object[] values)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            return DynamicQueryable.OrderBy(query, ordering, values);
        }

        public static IQueryable<T> WhereIf<T>(IQueryable<T> query, bool condition, Expression<Func<T, bool>> func)
        {
            return condition ? query.Where(func) : query;
        }

        public static IQueryable<T> PageBy<T>(IQueryable<T> query, int skipCount, int MaxResultCount)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            return query.Skip(skipCount).Take(MaxResultCount);
        }
    }
}