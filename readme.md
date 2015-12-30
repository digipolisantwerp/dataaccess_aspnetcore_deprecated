# DataAccess Toolbox

The DataAccess Toolbox contains the base classes for data access in ASP.NET 5 with Entity Framework using the unit-of-work and repository pattern.

It contains :
- base classes for entities.
- base classes for repositories.
- unit-of-work and repository pattern.
(- automatic discovery of repositories -- not yet)


## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Installation](#installation)
- [Configuration](#configuration)
  - [Path to json config file](#path-to-json-config-file)
  - [DataAccessOptions](#dataaccessoptions)
- [
](#)
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
    ...,
    "Toolbox.DataAccess":  "1.3.0", 
    ...
 }
```

Alternatively, it can also be added via the NuGet Package Manager interface.

## Configuration

The DataAccess framework is registered and configured in the ConfigureServices method of the Startup class.

There are 2 ways to configure the DataAccess framework :
- by passing a path to a json config file to the AddDataAccess method
- by passing a DataAccessOptions instance to the AddDataAccess method

### Path to json config file 

The DataAccess framework will read the json file and create a DataAccessOptions instance.

``` csharp
services.AddDataAccess<MyEntityContext>("path-to-config.json");
```

The concrete type of your DbContext has to be passed as generic parameter to the AddDataAccess method.

The config file has to contain the following section :

``` json
{
	"ConnectionString": {
		"Host": "localhost",
		"Port": "1234",
		"DbName": "dbname",
		"User": "user",
		"Password":  "pwd"
	}
}
```
Port is optional, it can be omitted. If it is included it must contain a valid port number (numeric value from 0 to 65535).

### DataAccessOptions

You can also instantiate and populate a DataAccessOptions object yourself and pass it directly to the AddDataAccess method.

``` csharp
var connString = new ConnectionString("host", 1234, "dbname", "user", "pwd");
var dataAccessOptions = new DataAccessOptions(connString);
services.AddDataAccess<MyEntityContext>(dataAccessOptions);
```

When you don't need to specify a port in the connection string, pass 0 to it.  

------------------------------


Een configuratie moet ingeladen worden in de Configure method van de Startup class, hierin wordt de DbConfiguration geladen, standaard zal de PostgresDbConfiguration geladen worden zoals gedefineerd in onderstaand voorbeeld

``` csharp
    app.UseDataAccess();
```

De PostgresDbConfiguration class
``` csharp
    public class PostgresDbConfiguration : DbConfiguration
    {
        public PostgresDbConfiguration()
        {
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
            SetProviderFactory("Npgsql", Npgsql.NpgsqlFactory.Instance);
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
        }
    }
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


