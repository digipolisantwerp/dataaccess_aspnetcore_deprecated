# DataAccess Toolbox

The DataAccess Toolbox contains the base classes for data access in ASP.NET 5 with Entity Framework 6.1 using the unit-of-work and repository pattern.

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
  - [Json config file](#json-config-file)
  - [Code](#code)
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
- [Paging](#paging)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

Adding the DataAccess Toolbox to a project is as easy as adding it to the project.json file :

``` json
 "dependencies": {
    "Toolbox.DataAccess":  "1.6.0", 
 }
```

Alternatively, it can also be added via the NuGet Package Manager interface.

## Configuration in Startup.ConfigureServices

The DataAccess framework is registered in the _**ConfigureServices**_ method of the *Startup* class.

There are 2 ways to configure the DataAccess framework :
- using a json config file
- using code

### Json config file 

The path to the Json config file has to be given as argument to the _*AddDataAccess*_ method, together with the concrete type of your DbContext as generic parameter :

``` csharp
services.AddDataAccess<MyEntityContext>(opt => opt.FileName = "configs/dbconfig./json");

```

If the DataAccess section in your json file is not named 'DataAccess' (=default), also pass in your custom section name :

``` csharp
services.AddDataAccess<MyEntityContext>(opt => 
                                        { 
                                            opt.FileName = "configs/dbconfig./json"; 
                                            opt.SectionName = "MyDataAccessSection"; 
                                        });
```

The DataAccess framework will read the given section of the json file with the following structure :

``` json
{
    "DataAccess": {
        "ConnectionString": {
            "Host": "localhost",
            "Port": "1234",
            "DbName": "dbname",
            "User": "user",
            "Password":  "pwd"
        },
        "LazyLoadingEnabled": false,
        "PluralizeTableNames": false,
        "DefaultSchema":  "schemaname"
    }
}
```
Port is optional. If it is included it must contain a valid port number (numeric value from 0 to 65535).
PluralizeTableNames is optional, the default value is true.
DefaultSchema is optional, the default value is "dbo".

### Code

You can also call the _*AddDataAccess*_ method, passing in the needed options directly : 

``` csharp
var connString = new ConnectionString("host", 1234, "dbname", "user", "pwd");
services.AddDataAccess<MyEntityContext>(opt => opt.ConnectionString = connString);
```

When you don't need to specify a port in the connection string, pass 0 to it.  

### NpgSql

If you use NpgSql, you must also pass in a DbConfiguration object that contains the Npgsql configuration :

``` csharp 
public class PostgresDbConfiguration : DbConfiguration
{
    public PostgresDbConfiguration()
    {
        SetDefaultConnectionFactory( new Npgsql. NpgsqlConnectionFactory());
        SetProviderFactory( "Npgsql", Npgsql.NpgsqlFactory .Instance);
        SetProviderServices( "Npgsql", Npgsql.NpgsqlServices .Instance);
    }
}
``` 

You can create this class yourself in your project, or you can include the Toolbox.DataAccess.Postgres package that contains this class.

In Startup.ConfigureServices : 

``` csharp 
var dbConfig = new PostgresDbConfiguration();

var connString = new ConnectionString("host", 1234, "dbname", "user", "pwd");
services.AddDataAccess<MyEntityContext>(opt => 
                                        { 
                                            opt.ConnectionString = connString; 
                                            opt.DbConfiguration = dbConfig;
                                        });

// or when using a config file

services.AddDataAccess<MyEntityContext>(opt => 
                                        { 
                                            opt.FileName = "configs/dbconfig./json"; 
                                            opt.DbConfiguration = dbConfig; 
                                        });
```

## EntityContext

You inherit a DbContext object from the EntityContextBase class in the toolbox. This context contains all your project-specific DbSets and custom logic.   
The constructor has to accept an IOptions&lt;EntityContextOptions&gt; as input parameter, to be passed to the base constructor. This EntityContextOptions object is constructed from the DataAccess options, configured in the Startup class (see higher).

For example :  

``` csharp
    public class EntityContext : EntityContextBase
    {
        public EntityContext(IOptions<EntityContextOptions> options) : base(options)
        { }

        public DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<MyOtherEntity> MyOtherEntities { get; set; }
        
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
 
You can pass in false if you don't want the change tracking to activate (better performance when you only want to retrieve data and not insert/update/delete).  
 
Now Access your data via repositories :    
 
``` csharp  
var repository = uow.GetRepository<MyEntity>();
// your data access code via the repository comes here
```  

The UnitOfWork will be automatically injected in the repository and use it to interact with the database.  

When you want to submit changes to the database, call the SaveChanges or SaveChangesAsync method of the IUnitOfWork :  

``` csharp  
uow.SaveChanges();
```  

## Repositories

The toolbox registers generic repositories in the ASP.NET 5 DI container. They provide the following methods :    

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
public class MyRepository<MyEntity> : EntityRepositoryBase<MyDbContext, MyEntity>, IMyRepository
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

