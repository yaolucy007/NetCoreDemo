using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IUnitOfWork
    {
        string ClientID { get; set; }

        void RegisterAdd(BaseEntity entity, Action act);

        void RegisterUpdate(BaseEntity entity, Action act);

        void RegisterDelete(BaseEntity entity, Action act);

        void ClearAdd();

        void ClearDelete();

        void ClearUpdate();

        void Commit();

        string GetClientID();
    }
}
