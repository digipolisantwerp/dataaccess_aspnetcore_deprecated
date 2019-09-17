# DataAccess Toolbox Changelog

## 4.0.1

- fixed dependency injection memory leak

## 4.0.0

- update dependencies for .Net Standard 2.0

## 3.0.1

- bugfix: columnattribute now works when using LowerCaseTablesAndFields modelbuilder extension

## 3.0.0

- conversion to csproj and MSBuild.

## 2.5.2

- Recompilation to correct wrong dependencies.

## 2.5.1

- Bugfix invalid cast exception when creating custom repositories.

## 2.5.0

- Bugfix invalid cast exception when using GetCustomRepository.

## 2.4.0

- Added ModelBuilder extensions.

## 2.3.1

- Added Any and AnyAsync methods to Repository.

## 2.3.0

- Removed EntityBase constraint on multiple generic interfaces and classes.
- Added PageNumber, PageLength and TotalPageCount and renamed TotalCount to TotalEntityCount on DataPage class.

## 2.2.1

- Bugfix includes with Get and GetAsync.

## 2.2.0

- NoTracking behaviour.

## 2.1.0

- Changed include functionality.

## 2.0.0

- Upgrade to dotnet core 1.0.0.

## 1.6.2

- Re-Added GetRepository<TRepository> (removed in 1.6.0).

## 1.6.0

- Added PluralizeTableNames to EntityContextOptions.
- Added DefaultSchema to EntityContextOptions.
- Changed generic implementation of GetRepository.
- Changed dependencies to logger from ILogger to ILogger of DataAccess.

## 1.5.0

- Added paging.

## 1.4.0

- Upgrade to ASP.NET 5 RC1.
