using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System;

namespace Common
{
    public enum ModelDelete{
        已删除=1,
        未删除=0
    }
public class BaseModel
    {
        [BsonId(IdGenerator =typeof(StringObjectIdGenerator)),BsonElement("_id")]
        [JsonIgnore]

        public string _id { get; set; }
        [BsonElement("CreateTime")]
        [JsonIgnore]
        public DateTime CreateTime { get; set; } 
        [BsonElement("UpdateTime")]
        [JsonIgnore]
        public DateTime? UpdateTime { get; set; }

        [BsonElement("Creator")]
        [JsonIgnore]
        public string CreateUser { get; set; }

        [BsonElement("IsDelete")]
        [JsonIgnore]
        public ModelDelete? IsDelete { get; set; }

        public void Create() {
            CreateTime = System.DateTime.Now;
            CreateUser = "朱雄";
            IsDelete = ModelDelete.未删除;
        }
        public void Update() {
            UpdateTime = System.DateTime.Now;
        }
    }
}