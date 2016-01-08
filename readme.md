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
- [Gebruik](#gebruik)
  - [Get](#get)
  - [GetAll](#getall)
  - [Insert](#insert)
  - [Update](#update)
  - [Delete](#delete)
- [Versies](#versies)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

Adding the DataAccess Toolbox to a project is as easy as adding it to the project.json file :

``` json
 "dependencies": {
    "Toolbox.DataAccess":  "1.3.0", 
 }
```

Alternatively, it can also be added via the NuGet Package Manager interface.

## Configuration in Startup.ConfigureServices

The DataAccess framework is registered in the _*ConfigureServices*_ method of the *Startup* class.

There are 2 ways to configure the DataAccess framework :
- using a json config file
- using code

### Json config file 

The path to the Json config file has to be given as argument to the _*AddDataAccess*_ method, together with the concrete type of your DbContect as generic parameter :

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
        "LazyLoadingEnabled": false
    }
}
```
Port is optional. If it is included it must contain a valid port number (numeric value from 0 to 65535).

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

Startup.ConfigureServices : 

``` csharp 
var dbConfig = new PostgresDbConfiguration();

var connString = new ConnectionString("host", 1234, "dbname", "user", "pwd");
services.AddDataAccess<MyEntityContext>(opt => 
                                        { 
                                            opt.ConnectionString = connString; 
                                            opt.DbConfiguration = dbConfig;
                                        });

// or

services.AddDataAccess<MyEntityContext>(opt => 
                                        { 
                                            opt.FileName = "configs/dbconfig./json"; 
                                            opt.DbConfiguration = dbConfig; 
                                        });
```

## EntityContext

Geef een EntityContext mee die overerft van Digipolis.DataAccess.Entiteiten.EntityContextBase. Deze Context dient DBSet properties van alle entiteiten te bevatten en kan aangevuld worden met 
eventuele startup instructies. Een constructor die een DataAccessOptions object injecteert is vereist, de DataAccessOptions dienen doorgegeven te worden aan de EntityContextBase.
Een voorbeeld van een EntityContext:

``` csharp
    public class EntityContext : EntityContextBase
    {
        public EntityContext(DataAccessOptions dataAccessOptions) 
            : base(dataAccessOptions)
        {
            _dataAccessOptions = dataAccessOptions;
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<Locatie> Locaties { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			...  Zie documentatie entity framework
		}


	}
```

## Entities

Entities dienen over te erven van Digipolis.DataAcces.EntityBase en geregistreerd te worden in de EntityContext (zie voorbeeld hierboven). EntityBase bevat een int Id met Key attribuut. Is vereist om te kunnen gebruiken in onder andere IRepository.

## Gebruik

Voorbeelden van gebruik

### Get

``` csharp
    public virtual async Task<Afwezigheid> GetAsync(int id, IncludeList<Afwezigheid> includes)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<IRepository<Afwezigheid>>();
                return await repository.GetAsync(id, includes: includes);
            }
        }
```

Voorbeeld van een IncludeList die voor de Afwezigheid entiteit ook Deelnmer en SoortAfwezigheid volledig gaat ophalen:
``` csharp
   var includeList = new IncludeList<Afwezigheid>(a => a.Deelnemer, a => a.SoortAfwezigheid);
```


### GetAll
``` csharp
    public async Task<IEnumerable<Afwezigheid>> GetAllAsync(IncludeList<Afwezigheid> includes)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<IRepository<Afwezigheid>>();
                return await repository.GetAllAsync(includes: includes);
            }
        }
```


### Insert
``` csharp
    public async Task InsertAsync(Deelnemer nieuweDeelnemer)
        {
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<IRepository<Deelnemer>>();
                repository.Add(nieuweDeelnemer);
                await uow.SaveChangesAsync();
            }
        }
```


### Update
``` csharp
    public async Task UpdateAsync(Deelnemer nieuweDeelnemer)
        {
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<IRepository<Deelnemer>>();
                repository.Update(nieuweDeelnemer);
                await uow.SaveChangesAsync();
            }
        }
```


### Delete
``` csharp
    public async Task UpdateAsync(Deelnemer nieuweDeelnemer)
        {
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<IRepository<Deelnemer>>();
                repository.Remove(nieuweDeelnemer);
                await uow.SaveChangesAsync();
            }
        }
```

of

``` csharp
    public async Task UpdateAsync(Deelnemer nieuweDeelnemer)
        {
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<IRepository<Deelnemer>>();
                repository.Remove(nieuweDeelnemer.Id);
                await uow.SaveChangesAsync();
            }
        }
```

## Versies

| Versie | Auteur                                  | Omschrijving
| ------ | ----------------------------------------| ----------------------------------------------------
| 1.0.0  | Steven Vanden Broeck                    | Initiële versie.
| 1.2.0  | Sven Noreillie                          | Vervangt DataAccess folder


