using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Digipolis.DataAccess;
using DataAccess.SampleApp.Entities;
using Digipolis.DataAccess.Query;

namespace DataAccess.SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUowProvider _uowProvider;

        public HomeController(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public async Task<IActionResult> Index()
        {
            //await Seed();
            IEnumerable<Building> buildings = null;

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Building>();

                //**************************************

                var includes = new Includes<Building>(query =>
                {
                    return query.Include(b => b.Appartments)
                                    .ThenInclude(a => a.Rooms);
                });

                buildings = await repository.GetAllAsync(null, includes.Expression);

                //var building = await repository.GetAsync(1, includes.Expression);

                //**************************************

                //Func<IQueryable<Building>, IQueryable<Building>> func = query =>
                //{
                //    return query.Include(b => b.Appartments)
                //                    .ThenInclude(a => a.Rooms);
                //};

                //buildings = await repository.GetAllAsync(null, func);

                //**************************************

                //buildings = await repository.GetAllAsync(null, query =>
                //{
                //    return query.Include(b => b.Appartments)
                //                    .ThenInclude(a => a.Rooms);
                //});


            }

            return View(buildings);
        }

        public async Task<IActionResult> Seed()
        {
            var buildings = new List<Building>
            {
                new Building
                {
                    Name = "Building one",
                    Appartments = new List<Appartment>
                    {
                        new Appartment
                        {
                            Number = 1,
                            Floor = 1,
                            Rooms = new List<Room>
                            {
                                new Room
                                {
                                    Name = "Kitchen",
                                },
                                new Room
                                {
                                    Name = "Living",
                                },
                                new Room
                                {
                                    Name = "Bedroom",
                                }
                            }
                        },
                        new Appartment
                        {
                            Number = 21,
                            Floor = 2,
                            Rooms = new List<Room>
                            {
                                new Room
                                {
                                    Name = "Kitchen",
                                },
                                new Room
                                {
                                    Name = "Living",
                                },
                                new Room
                                {
                                    Name = "Bedroom 1",
                                },
                                new Room
                                {
                                    Name = "Bedroom 2",
                                }
                            }
                        }
                    }
                }
            };

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Building>();

                foreach (var item in buildings)
                {
                    repository.Add(item);
                }

                await uow.SaveChangesAsync();
            }

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
