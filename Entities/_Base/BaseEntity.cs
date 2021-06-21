using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public interface IEntity
    {

    }

    public class BaseEntity : IEntity
    {
        public string ClientID { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int AutoID { get; set; }

        public BaseEntity()
        {

        }
    }
}
