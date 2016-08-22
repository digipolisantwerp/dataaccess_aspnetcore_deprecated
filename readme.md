# DataAccess Toolbox

The DataAccess Toolbox contains the base classes for data access in ASP.NET Core with Entity Framework Core 1.0 using the unit-of-work and repository pattern.

It contains :
- base classes for entities.
- base classes for repositories.
- unit-of-work and repository pattern.
(- automatic discovery of repositories -- not yet)


## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Installation](#installation)
- [Configuration in Startup.ConfigureServices](#configuration-in-startupconfigureservices)
  - [NpgSql](#npgsql)
- [EntityContext](#entitycontext)
- [Entities](#entities)
- [UnitOfWork](#unitofwork)
- [Repositories](#repositories)
  - [Get and GetAsync](#get-and-getasync)
  - [GetAll and GetAllAsync](#getall-and-getallasync)
  - [Add](#add)
  - [Update](#update)
  - [Remove](#remove)
  - [Custom Repositories](#custom-repositories)
- [Query](#query)
  - [Filter](#filter)
  - [Includes](#includes)
  - [OrderBy](#orderby)
- [Paging](#paging)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->
## Installation

Adding the DataAccess Toolbox to a project is as easy as adding it to the project.json file :

``` json
 "dependencies": {
    "Digipolis.DataAccess":  "2.3.0",
 }
```

Alternatively, it can also be added via the NuGet Package Manager interface.

## Configuration in Startup.ConfigureServices

The DataAccess framework is registered in the _**ConfigureServices**_ method of the *Startup* class.


``` csharp
services.AddDataAccess<MyEntityContext>();
```

Next to this registration you will need to register entity framework separately.

### NpgSql

If you use NpgSql, you can use this entity framework configuration:

``` csharp
var connection = @"Server=127.0.0.1;Port=5432;Database=TestDB;User Id=postgres;Password=mypwd;";
services.AddDbContext<MyEntityContext>(options => options.UseNpgsql(connection));
```

Check the [Entity Framework documentation](https://ef.readthedocs.io/en/latest/) for more info on the configuration possibilities.


## EntityContext

You inherit a DbContext object from the EntityContextBase class in the toolbox. This context contains all your project-specific DbSets and custom logic.
The constructor has to accept an DbContextOptions&lt;TContext&gt; as input parameter, to be passed to the base constructor and where TContext is the type of your context.
The EntityContextBase is a generic type with your context as type parameter. This is necessary in order to pass the generic DbContextOptions object to the underlying DbContext constructor.

For example :

``` csharp
public class EntityContext : EntityContextBase<EntityContext>
{
    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    { }

    public DbSet<MyEntity>
    MyEntities { get; set; }
    public DbSet<MyOtherEntity>
    MyOtherEntities { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // optional, see Entity Framework documentation
    }
}
```

## Entities

Your Entities are inherited from the base class EntityBase in the toolbox :

``` csharp
public class MyEntity : EntityBase
{
    public string MyProperty { get; set; }
}
```

The EntityBase class already contains an int property, named Id.

## UnitOfWork

The toolbox contains a UnitOfWork class and IUnitofWork interface that encapsulates the DbContext and that you use in your business classes to separate the data access code from business code.
The IUnitOfWork is instantiated by the IUowProvider.

First inject the IUowProvider in your business class :

``` csharp
public class BusinessClass
{
    public BusinessClass(IUowProvider uowProvider)
    {
        _uowProvider = uowProvider;
    }

    private readonly IUowProvider _uowProvider;
}
```

Then ask the IUowProvider for a IUnitOfWork :

``` csharp
using ( var uow = _uowProvider.CreateUnitOfWork() )
{
    // your business logic that needs dataaccess comes here
}
```

You can pass in false if you don't want the **change tracking** to activate (better performance when you only want to retrieve data and not insert/update/delete).

Now access your data via repositories :

``` csharp
var repository = uow.GetRepository<MyEntity>();
// your data access code via the repository comes here
```

The UnitOfWork will be automatically injected in the repository and used to interact with the database.

To persist your changes to the database, call the SaveChanges or SaveChangesAsync method of the IUnitOfWork :

``` csharp
uow.SaveChanges();
```

## Repositories

The toolbox registers generic repositories in the ASP.NET Core DI container. They provide the following methods :

### Get and GetAsync

Retrieve a single record by id, optionally passing in an IncludeList of child entities that you also want retrieved :

``` csharp
using (var uow = _uowProvider.CreateUnitOfWork())
{
    var repository = uow.GetRepository<MyEntity>();

    // retrieve MyEntity with id = 5
    var entity = repository.Get(5);

    // retrieve MyEntity with id 12 and its child object
    var includeList = new IncludeList<MyEntity>(e => e.Child);
    var entity2 = repository.Get(12, includes: includeList);
}
```

### GetAll and GetAllAsync

Retrieves all records, with or without child records.

``` csharp
using (var uow = _uowProvider.CreateUnitOfWork(false))
{
    var repository = uow.GetRepository<MyEntity>();
    var entities = repository.GetAll(includes: includes);
}
```

### Add

Adds a record to the repository. The record is persisted to the database when calling IUnitOfWork.SaveChanges().

``` csharp
using (var uow = _uowProvider.CreateUnitOfWork())
{
    var repository = uow.GetRepository<MyEntity>();
    repository.Add(newEntity);
    uow.SaveChanges();
}
```

### Update

Updates an existing record.

Important note! When you update an entity with children objects, these child objects will also be updated. There is no need to update al the child objects separately.
This is a new behaviour from the entity framework core **update** method on the DbSet.

``` csharp
using (var uow = _uowProvider.CreateUnitOfWork())
{
    var repository = uow.GetRepository<MyEntity>();
    repository.Update(updatedEntity);
    await uow.SaveChangesAsync();
}
```

### Remove

You can call this method with an existing entity :

``` csharp
using (var uow = _uowProvider.CreateUnitOfWork())
{
    var repository = uow.GetRepository<MyEntity>();
    repository.Remove(existingEntity);
    await uow.SaveChangesAsync();
}
```

Or with the Id of an existing entity :

``` csharp
using (var uow = _uowProvider.CreateUnitOfWork())
{
    var repository = uow.GetRepository<MyEntity>();
    repository.Remove(id);
    uow.SaveChangesAsync();
}
```

### Custom Repositories

When you need more functionality in a repository than the generic methods, you can create our own repositories by inheriting from the repository base classes.

To make a repository that is tied to 1 entity type, you inherit from the _**EntityRepositoryBase**_ class :

``` csharp
public class MyRepository<MyEntity> : EntityRepositoryBase<MyDbContext, MyEntity> , IMyRepository
{
    public MyRepository(ILogger logger) : base(logger, null)
    { }
}
```

This base class already contains the generic Add, Update, Delete, Get and Query methods.

If you just want to start with an empty repository or a repository that is not tied to 1 type of entity, inherit from the _**RepositoryBase**_ class :

``` csharp
public class MyRepository : RepositoryBase, IMyRepository
{
    public MyRepository(ILogger logger) : base(logger, null)
    { }
}
```

**Don't forget to register your own repositories in the DI container at startup :**

``` csharp
services.AddTransient<IMyRepository, MyRepository>();        // or any other scope (Scoped, Singleton).
```

## Query
These helpers help generating queries:

### Filter
A Filter holds the requested filter (WHERE) values.

``` csharp
List<`Participant`> participants = null;

using (var uow = _uowProvider.CreateUnitOfWork(false))
{
    Filter<Entities.Participant>
    filter = new Filter<Entities.Participant>
    (null);

    filter.AddExpression(e => idList.Contains(e.Id));

    var repository = uow.GetRepository<IRepository<Participant>>();

    participants = (await repository.QueryAsync(filter.Expression, includes:includes)).ToList();
}
```

### Includes
Holds the parameters to generate the Include part of the query.

``` csharp

var includes = new Includes<Building>(query =>
{
    return query.Include(b => b.Appartments)
                    .ThenInclude(a => a.Rooms);
}); 

buildings = await repository.GetAllAsync(null, includes.Expression);

```

### OrderBy
Holds the parameters to generate the OrderBy part of the query.

``` csharp

var orderBy = new OrderBy<Entities.Participant>(string.IsNullOrEmpty(paging.Sort) ? "Reservation.DateOfReservation" : paging.Sort, paging.Descending);

```

## Paging

When working with large collections of data you'll want to keep your application performant. Instead of retrieving all records, you can serve your data
to the consumer in pages.

The repositories have methods that can be used with paging systems. You can also inject a IDataPager object in your classes to retrieve paged data :

``` csharp
public class MyBusinessClass
{
    public MyBusinessClass(IDataPager<MyEntity> pager)
    {
        _pager = pager;
    }

    private readonly IDataPager<MyEntity> _pager;
}
```

and call its methods to retrieve paged data :

``` csharp
var pageNumber = 1;
var pageLength = 10;

var data = _pager.Get(pageNumber, pageLength);

var filter = new Filter<MyEntity>(e => e.AProperty == true);

var filteredData = _pager.Query(pageNumber, pageLenght, filter);
```
