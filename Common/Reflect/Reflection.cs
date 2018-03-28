using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Common
{
    public class ReflectEntity
    {
        public string EntityName { get; set; }
        public object EntityValue { get; set; }
        public Type EntityType { get; set; }
        public IEnumerable<ReflectEntity> reflectEntity { get; set; }
    }
    public static class Reflection
    {
       
        public static List<ReflectEntity> R_ReflectModel(this object model) {
            List<ReflectEntity> pairs = new List<ReflectEntity>();
            var props = model.GetType().GetProperties();
            foreach (var prop in props)
            {
                var pair = new ReflectEntity();
                if (!prop.PropertyType.Assembly.FullName.Contains("mscorlib"))
                {
                    pair.reflectEntity= prop.R_ReflectModel();
                    pair.EntityName = prop.Name;
                    pair.EntityValue = prop.GetValue(model, null);
                    pair.EntityType = prop.GetType();
                    pairs.Add(pair);
                }
                else { 
                pair.EntityName = prop.Name;
                pair.EntityValue = prop.GetValue(model, null);
                pair.EntityType = prop.GetType();
                pairs.Add(pair);
                }
            }
            return pairs;
        }
    }
}
