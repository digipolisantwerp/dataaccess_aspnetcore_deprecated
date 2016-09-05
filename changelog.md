# DataAccess Toolbox Changelog

## 1.4.0

- Upgrade to ASP.NET 5 RC1

## 1.5.0

- Added paging.

## 1.6.0

- Added PluralizeTableNames to EntityContextOptions.
- Added DefaultSchema to EntityContextOptions.
- Changed generic implementation of GetRepository.
- Changed dependencies to logger from ILogger to ILogger of DataAccess

## 1.6.2

- Re-Added GetRepository<TRepository> (removed in 1.6.0)

## 2.0.0

- Upgrade to dotnet core 1.0.0

## 2.1.0

- Changed include functionality

## 2.2.0

- NoTracking behaviour

## 2.2.1

- Bugfix includes with Get and GetAsync

## 2.3.0

- Removed EntityBase constraint on multiple generic interfaces and classes.
- Added PageNumber, PageLength and TotalPageCount and renamed TotalCount to TotalEntityCount on DataPage class

## 2.3.1

- Added Any and AnyAsync methods to Repository