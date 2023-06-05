using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Electronic_goods.Models;

namespace Electronic_goods.Db
{
    public class CRUDOperation
    {
        readonly SQLiteConnection db;
        public CRUDOperation(string databasePath)
        {
            db = new SQLiteConnection(databasePath);
            db.CreateTable<Busket>();
            db.CreateTable<Tovars>();
            db.CreateTable<Client>();
            db.CreateTable<Order>();
            db.CreateTable<Courier>();
            db.CreateTable<Delivery>();
            db.CreateTable<Review>();
            db.CreateTable<ImproveOffer>();
            db.CreateTable<Report>();
        }
        public IEnumerable<Tovars> GetFurnituress()
        {
            return db.Table<Tovars>();
        }
        public IEnumerable<Client> GetClients()
        {
            return db.Table<Client>();
        }
        public IEnumerable<Busket> GetBasket()
        {
            return db.Table<Busket>();
        }
        public Tovars GetProjectItem(int id)
        {
            return db.Get<Tovars>(id);
        }
        public IEnumerable<Review> GetReviews()
        {
            return db.Table<Review>();
        }
        public IEnumerable<ImproveOffer> GetImproveOffers()
        {
            return db.Table<ImproveOffer>();
        }
        public IEnumerable<Tovars> GetTovars()
        {
            return db.Table<Tovars >();
        }
        public IEnumerable<Report> GetReport()
        {
            return db.Table<Report>();
        }

        public int DeleteTovarsInBasket(int id) { return db.Delete<Busket>(id); }
        public int DeleteRewiws(int id) { return db.Delete<Review>(id); }
        public int DeleteTovars(int id) { return db.Delete<Tovars>(id); }
        public int DeleteOffers(int id) { return db.Delete<ImproveOffer>(id); }
        public int DeleteReports(int id) { return db.Delete<Report>(id); }

        public int SaveTovars(Tovars projectModel)
        {
            if (projectModel.Id != 0)
            {
                db.Update(projectModel);
                return projectModel.Id;
            }
            else
            {
                return db.Insert(projectModel);
            }
        }
        public int SaveReport(Report projectModel)
        {
            if (projectModel.Id != 0)
            {
                db.Update(projectModel);
                return projectModel.Id;
            }
            else
            {
                return db.Insert(projectModel);
            }
        }

        public int SaveBasket(Busket projectModel)
        {
            if (projectModel.Id != 0)
            {
                db.Update(projectModel);
                return projectModel.Id;
            }
            else
            {
                return db.Insert(projectModel);
            }
        }
        public int SaveReview(Review projectModel)
        {
            if (projectModel.Id != 0)
            {
                db.Update(projectModel);
                return projectModel.Id;
            }
            else
            {
                return db.Insert(projectModel);
            }
        }
        public int SaveImprove(ImproveOffer projectModel)
        {
            if (projectModel.Id != 0)
            {
                db.Update(projectModel);
                return projectModel.Id;
            }
            else
            {
                return db.Insert(projectModel);
            }
        }

        public int SaveClient(Client projectModel)
        {
            if (projectModel.Id != 0)
            {
                db.Update(projectModel);
                return projectModel.Id;
            }
            else
            {
                return db.Insert(projectModel);
            }
        }
    }
}
