# Dotnet_assignment4_movie_characters
###### Created by Thanh Tran, Olav Rongved and Ali.

Assignment for Noroff Dotnet programme. The purpose of the project is to make a RESTful API that can store information about characters, movies and franchises.
The Code is also made to be able to easily expand on the features where we have used the repository pattern to structure the program. 

**Programming language and tools**
* C#
* Entity framework
* Swagger
* SQL Server 
* ASP.NET Core
* RESTful API

## Business Rules

- [x] One movie contains many character
- [x] A character can play in multiple movies, make a linking table that can be called MovieCharacter 
- [x] One movies belongs to one franchise
- [x] A franchise can contain many movies


## Data requirements

**Character**

- [x] Autoincremented id
- [x] Full name
- [x] Alias(if applicable)
- [x] Gender
- [x] Picture (URL to photo - do not store an image)

**Movie**

- [x] Autoincremented Id
- [x] Movie title
- [x] Genre (just a simple string of a comma sepereated genres, there is no genre modelling required as a base)
- [x] Release year
- [x] Director(just a string name, no director modelling required as a base)
- [x] Picture (URL to a movie poster)
- [x] Trailer (Youtube link most likely)

**Franchise**

- [x] Autoincremented Id
- [x] NameS
- [x] Description

## API Requirements

Full CRUD for
- [x] Movies
- [x] Characters
- [x] Franchises

- [x] Make sure related data is not deleted - foreign keys kan be set to null
- [x] Moviecharacter linking table have to remove the related entries when a Character or Movie is deleted

Additional endpoints 
- [ ] Get all the movies in a franchise
- [x] Get all the characters in a movie
- [ ] Get all the caracters in a franchise 